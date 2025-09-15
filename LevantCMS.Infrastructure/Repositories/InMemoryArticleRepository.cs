using LevantCMS.Application.Abstractions;
using LevantCMS.Domain.Entities;

namespace LevantCMS.Infrastructure.Repositories;

public class InMemoryArticleRepository : IArticleRepository
{
    private readonly List<Article> _articles = new();

    public Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var article = _articles.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(article);
    }

    public Task<List<Article>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_articles.ToList());
    }

    public Task CreateAsync(Article article, CancellationToken cancellationToken = default)
    {
        _articles.Add(article);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Article article, CancellationToken cancellationToken = default)
    {
        var index = _articles.FindIndex(a => a.Id == article.Id);
        if (index != -1)
        {
            _articles[index] = article;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _articles.RemoveAll(a => a.Id == id);
        return Task.CompletedTask;
    }
}