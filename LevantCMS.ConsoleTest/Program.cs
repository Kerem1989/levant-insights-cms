using LevantCMS.Application.Articles.Commands;
using LevantCMS.Application.Articles.Commands.CreateArticle;
using LevantCMS.Infrastructure.Repositories;
using LevantCMS.Domain.Entities;

// Setup repository + handler
var repo = new InMemoryArticleRepository();
var handler = new CreateArticleCommandHandler(repo);

// Create a new article
var command = new CreateArticleCommand("My First Article");
await handler.Handle(command, CancellationToken.None);

// Fetch all articles
var allArticles = await repo.GetAllAsync();

Console.WriteLine("Articles in repository:");
foreach (var article in allArticles)
{
    Console.WriteLine($"- {article.Id}: {article.Title}");
}