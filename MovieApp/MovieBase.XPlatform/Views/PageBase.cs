
using MovieBase.Common.Interfaces;

namespace MovieBase.XPlatform.Views;

public class PageBase : ContentPage, IQueryAttributable
{
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is INavigatable aware)
        {
            await aware.OnNavigatedTo(query as Dictionary<string, object>);
        }
    }
}