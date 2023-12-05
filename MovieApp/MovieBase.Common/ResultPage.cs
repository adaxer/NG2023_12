namespace MovieBase.Common;
public class ResultPage<T>
{
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public List<T> Data { get; set; } = new List<T>();
}
