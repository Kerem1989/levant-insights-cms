
using Application.Tags;
using Infrastructure.Repositories;
using System.Threading.Tasks;
using Xunit;

public class CreateTagCommandHandlerTests
{
    [Fact]
    public async Task Should_Create_Tag()
    {
        // Arrange
        var repo = new InMemoryTagRepository();
        var handler = new CreateTagCommandHandler(repo);
        var command = new CreateTagCommand(1, "DevOps");

        // Act
        await handler.Handle(command);
        var tag = await repo.GetByIdAsync(1);

        // Assert
        Assert.NotNull(tag);
        Assert.Equal("DevOps", tag!.Name);
    }
}
