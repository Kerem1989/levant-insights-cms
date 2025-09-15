using LevantCMS.Domain.Entities;

namespace LevantCMS.Application.Abstractions;

public interface IArticleRepository
{
    Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Article>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(Article article, CancellationToken cancellationToken = default);
    Task UpdateAsync(Article article, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
