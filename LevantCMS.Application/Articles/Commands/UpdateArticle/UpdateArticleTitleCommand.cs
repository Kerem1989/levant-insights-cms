namespace LevantCMS.Application.Articles.Commands.UpdateArticle
{
    public sealed record UpdateArticleTitleCommand(Guid ArticleId, string NewTitle);
}