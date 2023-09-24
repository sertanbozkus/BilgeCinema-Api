using BilgeCinema.Business.Dtos;
using BilgeCinema.Business.Services;
using BilgeCinema.Data.Entities;
using BilgeCinema.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeCinema.Business.Managers
{
    public class MovieManager : IMovieService
    {
        private readonly IRepository<MovieEntity> _movieRepository;
        public MovieManager(IRepository<MovieEntity> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public bool AddMovie(AddMovieDto addMovieDto)
        {
            var entity = new MovieEntity()
            {
                Name = addMovieDto.Name,
                Type = addMovieDto.Type,
                Director = addMovieDto.Director,
                UnitPrice = addMovieDto.UnitPrice
            };

            try
            {
                _movieRepository.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
             
            }

            // WebApi işlerinde form validation yeterli olmayabilir çünkü verilerin her zaman bir form üzerinden gönderileceğinin garantisi yok. Bu nedenle db_SaveChanges() patlıyor mu diye try-catch ile kontrol etmekte fayda var.

        }

        public int DeleteMovie(int id)
        {
            var entity = _movieRepository.GetById(id);

            if (entity is null)
                return 0;

            try
            {
                _movieRepository.Delete(entity);
                return 1;
            }
            catch (Exception)
            {
                return -1;

            }
        }

        public GetMovieDto GetMovie(int id)
        {
            var entity = _movieRepository.GetById(id);

            if (entity is null)
                return null;

            var getMovieDto = new GetMovieDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                Director = entity.Director,
                UnitPrice = entity.UnitPrice
            };

            return getMovieDto;
        }

        public List<GetMovieDto> GetMovies()
        {
            var movieEntites = _movieRepository.GetAll();

            var movieDtos = movieEntites.Select(x => new GetMovieDto
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Director = x.Director,
                UnitPrice = x.UnitPrice
            }).ToList();

            return movieDtos;
        }

        public int MakeDiscount(int id)
        {
            var entity = _movieRepository.GetById(id);

            if (entity is null)
                return 0;

            if (entity.IsDiscounted)
            {
                entity.UnitPrice = entity.UnitPrice * 2;
            }
            else
            {
                entity.UnitPrice = entity.UnitPrice / 2;
            }

            entity.IsDiscounted = !entity.IsDiscounted; // Durum indirimdeyse indirimde değil, indirimde değilse indirimde yapacak

            try
            {
                _movieRepository.Update(entity);
                return 1;
            }
            catch (Exception)
            {
                return -1;

            }



        }

        public int UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            var entity = _movieRepository.GetById(updateMovieDto.Id);

            if (entity is null)
                return 0;


            entity.Name = updateMovieDto.Name;
            entity.Type = updateMovieDto.Type;
            entity.Director = updateMovieDto.Director;
            entity.UnitPrice = updateMovieDto.UnitPrice;

            try
            {
                _movieRepository.Update(entity);
                return 1;
            }
            catch (Exception)
            {
                return -1;
                            
            }
        }
    }
}
