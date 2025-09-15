using LevantCMS.Application.Articles.Commands;
using LevantCMS.Application.Articles.Commands.CreateArticle;
using LevantCMS.Infrastructure.Repositories;
using Xunit;

namespace LevantCMS.Tests;

public class CreateArticleCommandHandlerTests
{
    [Fact]
    public async Task Should_Add_Article_To_Repository()
    {
        var repo = new InMemoryArticleRepository();
        var handler = new CreateArticleCommandHandler(repo);
        var command = new CreateArticleCommand("My First Article");

        await handler.Handle(command, CancellationToken.None);

        var articles = await repo.GetAllAsync();
        Assert.Single(articles);
        Assert.Equal("My First Article", articles[0].Title);
    }
    
    [Fact]
    public async Task Should_Update_Existing_Article()
    {
        var repo = new InMemoryArticleRepository();
        var handler = new CreateArticleCommandHandler(repo);

        var command = new CreateArticleCommand("Original Title");
        await handler.Handle(command, CancellationToken.None);

        var existing = (await repo.GetAllAsync()).First();
        existing.Title = "Updated Title";

        await repo.UpdateAsync(existing);

        var updated = await repo.GetByIdAsync(existing.Id);
        Assert.NotNull(updated);
        Assert.Equal("Updated Title", updated!.Title);
    }

}