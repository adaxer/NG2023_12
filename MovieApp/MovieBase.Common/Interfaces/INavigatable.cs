namespace MovieBase.Common.Interfaces;

public interface INavigatable
{
    Task OnNavigatedTo(Dictionary<string, object> parameters);
}

