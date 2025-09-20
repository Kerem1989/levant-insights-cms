namespace LevantCMS.Application.Articles.Commands.CreateArticle
{
    using System.Collections.Generic;

    public sealed record CreateArticleCommand(string Title, List<string>? Tags = null);
}