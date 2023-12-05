namespace MovieBase.Common;
public class Award
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property for the relationship
    public ICollection<Movie> Movies { get; set; } =new List<Movie>();
}