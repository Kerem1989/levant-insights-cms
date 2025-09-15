namespace LevantCMS.Domain.Entities;

public class Article
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public List<Tag> Tags { get; set; } = new();

}