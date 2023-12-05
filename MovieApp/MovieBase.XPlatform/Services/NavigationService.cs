using MovieBase.Common.Interfaces;

namespace MovieBase.XPlatform.Services;
public class NavigationService : INavigationService
{
    public Task GoBackAsync() => Shell.Current.GoToAsync("..");

    public Task NavigateAsync(string targetViewModel, Dictionary<string, object>? parameters)
    {
        return Shell.Current.GoToAsync(GetViewName(targetViewModel), true, parameters);
    }
    private ShellNavigationState GetViewName(string targetViewModel)
    {
        return targetViewModel.Replace("ViewModel", "Page");
    }
}