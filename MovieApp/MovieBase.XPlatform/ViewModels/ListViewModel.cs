
using CommunityToolkit.Mvvm.ComponentModel;
using MovieBase.Common;
using MovieBase.Common.Interfaces;
using System.Collections.ObjectModel;

namespace MovieBase.XPlatform.ViewModels;
public partial class ListViewModel : BaseViewModel
{
    private readonly IMovieService _movieService;

    [ObservableProperty]
    private ICollection<MovieDTO>? _movies;

    public ListViewModel(IMovieService movieService)
    {
        Title = "List";
        _movieService = movieService;
    }

    public override async Task OnNavigatedTo(Dictionary<string, object> parameters)
    {
        if (parameters.TryGetValue("Title", out var value))
        {
            Title = $"{Title} {value}";
        }
        await LoadMovies();
    }

    private async Task LoadMovies()
    {
        Movies = new ObservableCollection<MovieDTO>(await _movieService.GetMoviePage(20, 0));
        Title = $"Loaded {Movies.Count} Movies";
    }
}
