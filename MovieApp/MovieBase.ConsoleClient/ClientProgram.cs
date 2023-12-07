using MovieBase.ClientLib;
using MovieBase.Common;
using MovieBase.Common.Interfaces;

namespace MovieBase.ConsoleClient;

internal class Program
{
    static IMovieService service = new MovieService();
    static IMessageService signalR = new MessageService();

    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Rest");
            Console.ReadLine();
            var restClient = new MovieApp.Rest.Client("https://localhost:7267", new HttpClient());
            await restClient.MovieListAsync(20, 4);

            Console.Write("Not found");
            Console.ReadLine();
            var result = await new HttpClient().GetAsync("https://localhost:7267/something");

            Console.WriteLine("\n\nMovieliste");
            var movies = await service.GetMoviePage(20, 0);

            foreach (var item in movies)
            {
                Console.WriteLine(item.Info);
            }

            var signalRConnected = await signalR.Connect();
            if (signalRConnected)
            {
                signalR.OnMessage += s =>
                {
                    Console.WriteLine(s);
                };
            }

            Console.WriteLine("\n\nEinzelnes Movie");
            if (!await TryLoadMovie())
            {
                await RegisterAndLogin();
                await TryLoadMovie();
            }

            Console.WriteLine("\n\nAdd Movie");
            await service.AddMovie(new Movie { Title = "Stargate", Director = "Whoever" }, CancellationToken.None);

            Console.WriteLine("\n\nPut");

            var success = await service.UpdateMovie(new Movie
            {
                Id = 1,
                Title = "Blade Runner 2048",
                Director = "Ridley",
                Released = DateOnly.FromDateTime(DateTime.Now.AddYears(-5))
            }, CancellationToken.None);
            Console.WriteLine($"Success? {success}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Typisch Demo-Dämon. Nach einfügen dieses Codes unten gehts auf einmal,
        // obwohl ich den Methodennamen im MessageService nicht geändert habe...
        Console.WriteLine("You can now send messages or hit return to exit");
        string? message;
        do
        {
            if ((message = Console.ReadLine()) != null)
            {
                await signalR.SendMessage(message);
            }

        } while (message != null);

        Console.ReadLine();
        await signalR.Disconnect();
    }

    private static async Task RegisterAndLogin()
    {
        await service.Register("test@test.de", "Pa$$w0rd");
        await service.Login("test@test.de", "Pa$$w0rd");
    }

    private static async Task<bool> TryLoadMovie()
    {
        try
        {
            var movie = await service.GetMovie(33);
            Console.WriteLine(movie.Title);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
            return false;
        }
    }
}
