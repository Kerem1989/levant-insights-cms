using LevantCMS.Domain.Entities;
using System.Threading.Tasks;


namespace Application.Abstractions;
    
    public interface ITagRepository
    {
        Task AddAsync(Tag tag);
        Task<Tag?> GetByIdAsync(int id);
        
        Task<Tag?> GetByNameAsync(string name);
        
        Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
    }