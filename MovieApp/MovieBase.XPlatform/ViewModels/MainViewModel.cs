using CommunityToolkit.Mvvm.Input;
using MovieBase.Common.Interfaces;

namespace MovieBase.XPlatform.ViewModels;
public partial class MainViewModel : BaseViewModel
{
    public MainViewModel(INavigationService navigation)
    {
        _navigation = navigation;
    }

    [RelayCommand]
    private async Task Navigate()
    {
        await _navigation.NavigateAsync("List", new Dictionary<string, object> { { "Title", "Called from Main" } });
    }

    private readonly INavigationService _navigation;
}
