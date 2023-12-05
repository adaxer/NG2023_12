using CommunityToolkit.Mvvm.ComponentModel;
using MovieBase.Common.Interfaces;

namespace MovieBase.XPlatform.ViewModels;

public abstract partial class BaseViewModel : ObservableObject, INavigatable
{
    [ObservableProperty]
    bool _isBusy = false;

    [ObservableProperty]
    string _title = string.Empty;

    public virtual Task OnNavigatedFrom(Dictionary<string, object> parameters) => Task.CompletedTask;

    public virtual Task OnNavigatedTo(Dictionary<string, object> parameters) => Task.CompletedTask;
}
