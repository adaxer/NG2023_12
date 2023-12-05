
namespace MovieBase.Common.Interfaces;
public interface IMovieService
{
    Task AddMovie(Movie movie, CancellationToken token);
    Task<Movie> GetMovie(int id);
    Task<IEnumerable<MovieDTO>> GetMoviePage(int pageSize, int pageNo);
    Task Login(string email, string password);
    Task Register(string email, string password);
    Task<bool> UpdateMovie(Movie movie, CancellationToken token);
}
