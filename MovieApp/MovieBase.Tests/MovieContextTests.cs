using Microsoft.EntityFrameworkCore;
using MovieBase.Common;
using MovieBase.Data;

namespace MovieBase.Tests;

public class MovieContextTests
{
    [Fact]
    public async Task DeleteMovie_ShouldCascadeDelete_MerchandisingsAndAwards()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieContext>()
            .UseInMemoryDatabase(databaseName: "MovieDbTest")
            .Options;
        Movie movie;

        // Use a separate instance of the context to setup the test data
        using (var setupContext = new MovieContext(options))
        {
            movie = new Movie { Title="Testmovie", Director="TestDirector" };
            var merchandising = new Merchandising { Movie = movie, Name="TestMerch" };
            var award = new Award();
            award.Movies.Add(movie);
            movie.Awards.Add(award);

            setupContext.Movies.Add(movie);
            setupContext.Merchandisings.Add(merchandising);
            setupContext.Awards.Add(award);
            await setupContext.SaveChangesAsync();
        }

        // Act
        using (var testContext = new MovieContext(options))
        {
            var movieToDelete = await testContext.Movies.FirstAsync(m=>m.Id==movie.Id);
            testContext.Movies.Remove(movieToDelete);
            await testContext.SaveChangesAsync();
        }

        // Assert
        using (var assertContext = new MovieContext(options))
        {
            Assert.False(await assertContext.Movies.AnyAsync(m => m.Id == movie.Id));
            Assert.False(await assertContext.Merchandisings.AnyAsync(m=>m.MovieId==movie.Id));
            Assert.False(await assertContext.Awards.Where(a => a.Movies.Any(m => m.Id == movie.Id)).AnyAsync());
        }
    }
}