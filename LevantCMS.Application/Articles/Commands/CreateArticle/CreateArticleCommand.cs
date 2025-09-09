using LevantCMS.Application.Abstractions;
using LevantCMS.Domain.Entities;


namespace LevantCMS.Application.Article.Commands.CreateArticle
{
    public sealed class CreateArticleCommandHandler
    {
        private readonly IArticleRepository _repository;

        public CreateArticleCommandHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task <Guid> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
        {
            Article article = new Article
            {
                Id = Guid.NewGuid(),
                Title = command.Title
            };


            await _repository.CreateAsync(article, cancellationToken);

            return article.Id;
        }
    }
}
