using Application.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LevantCMS.Domain.Entities;

namespace Infrastructure.Repositories;

    public class InMemoryTagRepository : ITagRepository
    {
        private readonly List<Tag> _tags = new();

        public Task AddAsync(Tag tag)
        {
            _tags.Add(tag);
            return Task.CompletedTask;
        }

        public Task<Tag?> GetByIdAsync(int id)
        {
            var tag = _tags.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(tag);
        }

        public Task<Tag?> GetByNameAsync(string name)
        {
            var tag = _tags.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
            return Task.FromResult(tag);
        }

        public Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_tags);
        }
    }