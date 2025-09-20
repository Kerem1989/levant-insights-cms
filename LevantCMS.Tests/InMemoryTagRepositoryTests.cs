using Infrastructure.Repositories;
using System.Threading.Tasks;
using LevantCMS.Domain.Entities;
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
    
    
}