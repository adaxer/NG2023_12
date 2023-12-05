namespace MovieBase.Common.Interfaces;

public interface INavigationService
{
    Task GoBackAsync();
    Task NavigateAsync(string name, Dictionary<string,object> parameters);
}

