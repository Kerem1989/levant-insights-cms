using LevantCMS.Application.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevantCMS.Application.Articles.Commands.UpdateArticle
{
    public sealed class UpdateArticleTitleCommandHandler
    {
        private readonly IArticleRepository _articleRepository;

        public UpdateArticleTitleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task Handle(UpdateArticleTitleCommand command, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new InvalidOperationException($"Article with ID {command.ArticleId} not found.");
            }

            article.Title = command.NewTitle;

            await _articleRepository.UpdateAsync(article, cancellationToken);
        }
    }
}