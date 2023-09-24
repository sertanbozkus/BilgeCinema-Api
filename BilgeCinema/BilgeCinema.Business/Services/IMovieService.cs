using BilgeCinema.Business.Dtos;

namespace BilgeCinema.Business.Services
{
    public interface IMovieService
    {
        bool AddMovie(AddMovieDto addMovieDto);

        List<GetMovieDto> GetMovies();

        GetMovieDto GetMovie(int id);

        int UpdateMovie(UpdateMovieDto updateMovieDto);

        int MakeDiscount(int id);

        int DeleteMovie(int id);
    }
}
