using BilgeCinema.Business.Dtos;
using BilgeCinema.Business.Services;
using BilgeCinema.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilgeCinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieRequest request)
        {
            var addMovieDto = new AddMovieDto()
            {
                Name = request.Name,
                Type = request.Type,
                Director = request.Director,
                UnitPrice = request.UnitPrice
            };


            var result = _movieService.AddMovie(addMovieDto);


            if (result)
                return Ok(); // return StatusCode(200)
            else
                return StatusCode(500);

        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            var getMovieDtos = _movieService.GetMovies();

            var response = getMovieDtos.Select(x => new GetMovieResponse
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Director = x.Director,
                UnitPrice = x.UnitPrice
            }).ToList();

            return Ok(response);

        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {

            var getMovieDto = _movieService.GetMovie(id);

            if (getMovieDto is null)
                return NotFound();

            var response = new GetMovieResponse()
            {
                Id = getMovieDto.Id,
                Name = getMovieDto.Name,
                Type = getMovieDto.Type,
                Director = getMovieDto.Director,
                UnitPrice = getMovieDto.UnitPrice
            };

            return Ok(response);

        }

   

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, UpdateMovieRequest request)
        {
            var updateMovieDto = new UpdateMovieDto()
            {
                Id = id,
                Name = request.Name,
                Type = request.Type,
                Director = request.Director,
                UnitPrice = request.UnitPrice
            };

            var result = _movieService.UpdateMovie(updateMovieDto);

            if (result == 0)
                return NotFound();
            else if (result == 1)
                return Ok();
            else
                return StatusCode(500);
           

        }

        [HttpPatch("{id}")]
        public IActionResult DiscountMovie(int id)
        {
            var result = _movieService.MakeDiscount(id);

            if (result == 0)
                return NotFound();
            else if (result == 1)
                return Ok();
            else
                return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var result = _movieService.DeleteMovie(id);

            if (result == 0)
                return BadRequest(); // Silme işleminde olmayan bir yapı silinmek istenirse BadRequest geriye dönülür.
            else if (result == 1)
                return Ok();
            else
                return StatusCode(500);
        }
    }
}
