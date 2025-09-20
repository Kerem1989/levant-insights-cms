using Application.Abstractions;
using LevantCMS.Application.Abstractions;
using LevantCMS.Domain.Entities;


namespace LevantCMS.Application.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandHandler
    {
        private readonly IArticleRepository _repository;
        private readonly ITagRepository _tagRepository;

        public CreateArticleCommandHandler(IArticleRepository repository,  ITagRepository tagRepository)
        {
            _repository = repository;
            _tagRepository = tagRepository;
        }

        public async Task <Guid> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
        {
            Article article = new Article
            {
                Id = Guid.NewGuid(),
                Title = command.Title
            };
            
            if (command.Tags != null)
            {
                foreach (var tagName in command.Tags)
                {
                    var existingTag = await _tagRepository.GetByNameAsync(tagName);

                    if (existingTag == null)
                    {
                        existingTag = new Tag { Name = tagName };
                        await _tagRepository.AddAsync(existingTag);
                        Console.WriteLine($"Created new tag: {existingTag.Name}");                    }
                    else
                    {
                        Console.WriteLine($"Found tag: {existingTag.Name}");
                    }
                    
                    article.Tags.Add(existingTag);
                    existingTag.Articles.Add(article);
                }
            }

            
            await _repository.CreateAsync(article, cancellationToken);

            return article.Id;
        }
        
    }
}
