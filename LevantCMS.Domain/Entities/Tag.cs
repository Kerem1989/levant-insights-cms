namespace LevantCMS.Domain.Entities;

public class Tag
{
    public int Id { get; set; }               
    public string Name { get; set; } = null!; 

    public List<Article> Articles { get; set; } = new();
}