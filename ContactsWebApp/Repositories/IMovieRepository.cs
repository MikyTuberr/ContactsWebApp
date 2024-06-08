using ContactsWebApp.Models;

namespace ContactsWebApp.Repositories
{
    public interface IMovieRepository 
    {
        public Task<List<Movie>?> GetAllAsync(); // get all movies collection
        public Task<Movie?> GetAsync(int id); // get movie by id

        public Task<int?> AddAsync(Movie movie); // add movie
        public Task<int?> EditAsync(int id, Movie movie); // edit movie by id

        public Task<int?> DeleteAsync(int id); // delete movie by id
        public Task<int> SaveChangesAsync(); // save changes to db
    }
}
