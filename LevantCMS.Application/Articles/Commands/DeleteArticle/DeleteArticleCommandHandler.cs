using LevantCMS.Application.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevantCMS.Application.Articles.Commands.DeleteArticle
{
    public sealed class DeleteArticleCommandHandler
    {
        private readonly IArticleRepository _articleRepository;

        public DeleteArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task Handle(DeleteArticleCommand command, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new InvalidOperationException($"Article with ID {command.ArticleId} not found.");
            }

            await _articleRepository.DeleteAsync(command.ArticleId, cancellationToken);
        }
    }
}