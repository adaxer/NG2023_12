using MovieBase.Common;
using MovieBase.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;

namespace MovieBase.ClientLib;

public class MovieService : IMovieService
{
    private string _baseUrl = "https://localhost:7267";
    private HttpClient _client = new HttpClient();
    private string? _accessToken = "";

    public async Task<IEnumerable<MovieDTO>> GetMoviePage(int pageSize, int pageNo)
    {
        var page = await _client.GetFromJsonAsync<ResultPage<MovieDTO>>($"{_baseUrl}/movies/list/{pageSize}/{pageNo}");
        return page!.Data!.ToList();
    }

    public async Task<Movie> GetMovie(int id)
    {
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        try
        {
            var movie = await _client.GetFromJsonAsync<Movie>($"{_baseUrl}/movies/{id}");
            return movie!;
        }
        catch (Exception ex)
        {
            Trace.TraceError($"{ex}");
            throw;
        }
    }

    public async Task AddMovie(Movie movie, CancellationToken token)
    {
        var result = await _client.PostAsJsonAsync<Movie>($"{_baseUrl}/movies", movie, token);
        Trace.WriteLine($"Status {result.StatusCode}");
        foreach (var item in result.Headers)
        {
            var value = string.Join(",", item.Value.ToArray());
            Trace.WriteLine($"Header {item.Key}: {value}");
        }
        var body = await result.Content.ReadAsStringAsync();
        Trace.WriteLine($"Body: {body}");
    }

    public async Task<bool> UpdateMovie(Movie movie, CancellationToken token)
    {
        var result = await _client.PutAsJsonAsync<Movie>($"{_baseUrl}/movies", movie, token);
        return (result.IsSuccessStatusCode);
    }

    public async Task Register(string email, string password)
    {
        var userData = new { username = email, password = password, email=email };
        var response = await _client.PostAsJsonAsync($"{_baseUrl}/register", userData);

        Trace.WriteLine($"Register returned {response.StatusCode}");
    }

    public async Task Login(string email, string password)
    {
        var userData = new { username = email, password = password, email = email };
        var response = await _client.PostAsJsonAsync($"{_baseUrl}/login", userData);

        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            _accessToken = tokenResponse?.AccessToken;
        }
    }
}

internal class TokenResponse
{
    public string? AccessToken { get; set; }
}
