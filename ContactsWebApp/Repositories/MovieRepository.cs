using ContactsWebApp.Data;
using ContactsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsWebApp.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context; // injected db context, provides communication with db

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int?> AddAsync(Movie movie)
        {
            if (_context.Movies == null) // check if "Movies" table isn't empty
            {
                return null;
            }
            _context.Movies.Add(movie); // add movie
            return await SaveChangesAsync();
        }

        public async Task<int?> EditAsync(int id, Movie updatedMovie)
        {
            if (_context.Movies == null) // check if "Movies" table isn't empty
            {
                return null;
            }

            var existingMovie = await GetAsync(id); // get movie by id

            if (existingMovie == null) // check if it exists
            {
                return null; 
            }

            // update values
            existingMovie.Title = updatedMovie.Title;
            existingMovie.Description = updatedMovie.Description;
            existingMovie.ReleaseDate = updatedMovie.ReleaseDate;

            _context.Entry(existingMovie).State = EntityState.Modified; // change state to modified
            return await SaveChangesAsync();
        }

        public async Task<List<Movie>?> GetAllAsync()
        {
            if (_context.Movies == null) // check if "Movies" table isn't empty
            {
                return null;
            }
            return await _context.Movies.ToListAsync(); // return list of movies
        }

        public async Task<Movie?> GetAsync(int id)
        {
            if (_context.Movies == null) // check if "Movies" table isn't empty
            {
                return null;
            }
            var movie = await _context.Movies.FindAsync(id); // find movie by id
            if(movie == null)
            {
                return null;
            }
            return movie;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            if (_context.Movies == null) // check if "Movies" table isn't empty
            {
                return null;
            }

            var movie = await GetAsync(id); // get movie by id

            if (movie == null)
            {
                return null;
            }

            _context.Movies.Remove(movie); // remove movie
            return await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
