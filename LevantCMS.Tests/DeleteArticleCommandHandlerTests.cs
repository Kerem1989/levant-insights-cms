using LevantCMS.Application.Articles.Commands.CreateArticle;
using LevantCMS.Application.Articles.Commands.DeleteArticle;
using Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using LevantCMS.Infrastructure.Repositories;
using Xunit;

public class DeleteArticleCommandHandlerTests
{
    [Fact]
    public async Task Should_Delete_Article()
    {
        // Arrange
        var articleRepo = new InMemoryArticleRepository();
        var tagRepo = new InMemoryTagRepository();
        var createHandler = new CreateArticleCommandHandler(articleRepo, tagRepo);

        var articleId = await createHandler.Handle(
            new CreateArticleCommand("To be deleted"),
            CancellationToken.None);

        var deleteHandler = new DeleteArticleCommandHandler(articleRepo);
        var deleteCommand = new DeleteArticleCommand(articleId);

        // Act
        await deleteHandler.Handle(deleteCommand, CancellationToken.None);
        var deletedArticle = await articleRepo.GetByIdAsync(articleId);

        // Assert
        Assert.Null(deletedArticle);
    }

    [Fact]
    public async Task Should_Throw_When_Deleting_NonExistent_Article()
    {
        // Arrange
        var articleRepo = new InMemoryArticleRepository();
        var deleteHandler = new DeleteArticleCommandHandler(articleRepo);

        var fakeId = Guid.NewGuid();
        var deleteCommand = new DeleteArticleCommand(fakeId);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            deleteHandler.Handle(deleteCommand, CancellationToken.None));
    }
}