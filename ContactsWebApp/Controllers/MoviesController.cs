using ContactsWebApp.DTO;
using ContactsWebApp.Models;
using ContactsWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContactsWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository; // injected movie repository, which does all db operations 

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // Get : api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _movieRepository.GetAllAsync(); // get movies
            if (movies == null)
            {
                return NotFound("Unable to find movies.");
            }
            return movies;
        }

        // Get : api/movies/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieRepository.GetAsync(id); // get movie by id
            if (movie == null)
            {
                return NotFound("Unable to find movie.");
            }
            return movie;
        }

        // Post : api/movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovieAsync(MovieDto movieDto)
        {
            if (!ModelState.IsValid) // validation of movieDto
            {
                return BadRequest(ModelState);
            }

            var movie = new Movie // create new movie entity, based on data from movieDto
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                ReleaseDate = movieDto.ReleaseDate
            };

            var result = await _movieRepository.AddAsync(movie); // add new movie

            if (result == null)
            {
                return NotFound("Unable to add the movie.");
            }

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie); // return 201 and added movie with it's id
        }

        // Put : api/movies/id
        [HttpPut]
        public async Task<ActionResult<Movie>> PutMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid) // validation of movieDto
            {
                return BadRequest(ModelState);
            }

            var movie = new Movie // create new movie entity, based on data from movieDto
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                ReleaseDate = movieDto.ReleaseDate
            };

            var result = await _movieRepository.EditAsync(id, movie); // edit movie by id

            if (result == null)
            {
                return NotFound("Unable to edit movie.");
            }
            return NoContent(); // return no content 204
        }

        // Delete : api/movies/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var result = await _movieRepository.DeleteAsync(id); // delete movie by id
            if (result == null)
            {
                return NotFound("Unable to delete movie.");
            }
            return NoContent(); // return no content 204
        }
    }
}
