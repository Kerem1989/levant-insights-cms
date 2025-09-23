using Infrastructure.Repositories;
using System.Threading.Tasks;
using LevantCMS.Application.Articles.Commands.CreateArticle;
using LevantCMS.Domain.Entities;
using LevantCMS.Infrastructure.Repositories;
using Xunit;

public class InMemoryTagRepositoryTests
{
    [Fact]
    public async Task Should_Add_Tag_To_Repository()
    {
        var repo = new InMemoryTagRepository();
        var tag = new Tag { Id = 1, Name = "Backend" };

        await repo.AddAsync(tag);
        var result = await repo.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Backend", result!.Name);
    }
    
    [Fact]
    public async Task Should_Get_Tag_By_Name()
    {
        var repo = new InMemoryTagRepository();
        var tag = new Tag { Id = 1, Name = "Gaza" };
        await repo.AddAsync(tag);
        var result = await repo.GetByNameAsync("Gaza");
        Assert.NotNull(result);
        Assert.Equal("Gaza", result!.Name);
        
    }
    
    
    [Fact]
    public async Task Should_Get_Tags_With_Linked_Articles_From_GetAll()
    {
        // Arrange
        var articleRepo = new InMemoryArticleRepository();
        var tagRepo = new InMemoryTagRepository();
        var handler = new CreateArticleCommandHandler(articleRepo, tagRepo);

        var command = new CreateArticleCommand(
            "Global Economy",
            new List<string> { "Economy", "Trade" }
        );

        // Act
        await handler.Handle(command, CancellationToken.None);
        var tags = await tagRepo.GetAllAsync();

        // Assert
        Assert.Equal(2, tags.Count);

        var economyTag = tags.Find(t => t.Name == "Economy");
        Assert.NotNull(economyTag);
        Assert.Single(economyTag!.Articles);
        Assert.Equal("Global Economy", economyTag.Articles[0].Title);

        var tradeTag = tags.Find(t => t.Name == "Trade");
        Assert.NotNull(tradeTag);
        Assert.Single(tradeTag!.Articles);
        Assert.Equal("Global Economy", tradeTag.Articles[0].Title);
    }
}
    
    