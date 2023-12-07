


using Microsoft.AspNetCore.SignalR;
using MovieBase.Common;
using MovieBase.Data;

namespace MovieBase.Api.Services;

public class AddMovieService : IHostedService
{
    private readonly ILogger<AddMovieService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AddMovieService(ILogger<AddMovieService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        DoWork();
        return Task.CompletedTask;
    }

    private async void DoWork()
    {
        _logger.LogInformation("Do Work");
        await Task.Delay(5000);
        while (true)
        {
            await AddMovie();
            await Task.Delay(20000);
        }
    }

    private async Task AddMovie()
    {
        using var scope = _serviceProvider.CreateScope();
 
        var db = scope.ServiceProvider.GetRequiredService<MovieContext>();
        Movie movie = new Movie { Title = $"New Movie at {TimeOnly.FromDateTime(DateTime.Now)}", Director = "Somebody" };
        db.Movies.Add(movie);
        await db.SaveChangesAsync();

        var hub = scope.ServiceProvider.GetRequiredService<IHubContext<MessageHub>>();
        await hub.Clients.All.SendAsync("message", $"New Movie released at {DateTime.Now}");
        
        _logger.LogInformation($"New Movie {movie.Title} released");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
