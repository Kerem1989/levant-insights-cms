using Infrastructure.Repositories;
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
        var tagRepo = new InMemoryTagRepository(); 
        var handler = new CreateArticleCommandHandler(repo, tagRepo);
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
        var tagRepo = new InMemoryTagRepository();
        var handler = new CreateArticleCommandHandler(repo, tagRepo);

        var command = new CreateArticleCommand("Original Title");
        await handler.Handle(command, CancellationToken.None);

        var existing = (await repo.GetAllAsync()).First();
        existing.Title = "Updated Title";

        await repo.UpdateAsync(existing);

        var updated = await repo.GetByIdAsync(existing.Id);
        Assert.NotNull(updated);
        Assert.Equal("Updated Title", updated!.Title);
    }
    
        [Fact]
        public async Task Should_Create_Article_With_Tags()
        {
            // Arrange
            var articleRepo = new InMemoryArticleRepository();
            var tagRepo = new InMemoryTagRepository();
            var handler = new CreateArticleCommandHandler(articleRepo, tagRepo);

            var command = new CreateArticleCommand(
                "Middle East Politics",
                new List<string> { "Politics", "Economy" }
            );

            // Act
            var articleId = await handler.Handle(command, CancellationToken.None);
            var article = await articleRepo.GetByIdAsync(articleId);

            // Assert
            Assert.NotNull(article);
            Assert.Equal("Middle East Politics", article!.Title);
            Assert.Equal(2, article.Tags.Count);

            // 👇 new: verify reverse navigation
            foreach (var tag in article.Tags)
            {
                Assert.Contains(article, tag.Articles);
            }
        }
}