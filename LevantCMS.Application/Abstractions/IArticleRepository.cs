using LevantCMS.Domain.Entities;

namespace LevantCMS.Application.Abstractions;

public interface IArticleRepository
{
    Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Article>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Article article, CancellationToken cancellationToken = default);
    Task UpdateAsync(Article article, CancellationToken cancellationToken = default);
    Task DeleteAsync(Article article, CancellationToken cancellationToken = default);
}
