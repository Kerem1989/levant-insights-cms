using Application.Abstractions;
using System.Threading.Tasks;
using LevantCMS.Domain.Entities;

namespace Application.Tags;

    public class CreateTagCommandHandler
    {
        private readonly ITagRepository _tagRepository;

        public CreateTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Handle(CreateTagCommand command)
        {
            var tag = new Tag
            {
                Id = command.Id,
                Name = command.Name
            };

            await _tagRepository.AddAsync(tag);
        }
    }