using LevantCMS.Application.Articles.Commands.CreateArticle;
using LevantCMS.Application.Articles.Commands.UpdateArticle;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LevantCMS.Infrastructure.Repositories;
using Xunit;

public class UpdateArticleTitleCommandHandlerTests
{
    [Fact]
    public async Task Should_Update_Article_Title()
    {
        // Arrange
        var articleRepo = new InMemoryArticleRepository();
        var tagRepo = new InMemoryTagRepository();
        var createHandler = new CreateArticleCommandHandler(articleRepo, tagRepo);

        var articleId = await createHandler.Handle(
            new CreateArticleCommand("Old Title", new List<string>()),
            CancellationToken.None);

        var updateHandler = new UpdateArticleTitleCommandHandler(articleRepo);
        var updateCommand = new UpdateArticleTitleCommand(articleId, "New Title");

        // Act
        await updateHandler.Handle(updateCommand, CancellationToken.None);
        var updatedArticle = await articleRepo.GetByIdAsync(articleId);

        // Assert
        Assert.NotNull(updatedArticle);
        Assert.Equal("New Title", updatedArticle!.Title);
    }
    
    [Fact]
    public async Task Should_Throw_When_Article_Not_Found()
    {
        // Arrange
        var articleRepo = new InMemoryArticleRepository();
        var handler = new UpdateArticleTitleCommandHandler(articleRepo);

        var fakeId = Guid.NewGuid();
        var command = new UpdateArticleTitleCommand(fakeId, "New Title");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}