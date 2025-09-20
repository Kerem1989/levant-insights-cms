using LevantCMS.Application.Articles.Commands;
using LevantCMS.Application.Articles.Commands.CreateArticle;
using LevantCMS.Infrastructure.Repositories;
using LevantCMS.Domain.Entities;

using LevantCMS.Application.Articles.Commands.CreateArticle;
using Infrastructure.Repositories;

// create repo + handler
var articleRepo = new InMemoryArticleRepository();
var tagRepo = new InMemoryTagRepository(); 
var handler = new CreateArticleCommandHandler(articleRepo, tagRepo); 


// create command with TagIds
var command = new CreateArticleCommand("Test Article with Tags", new List<string> {"politics", "economy"});

// run handler
await handler.Handle(command, CancellationToken.None);
