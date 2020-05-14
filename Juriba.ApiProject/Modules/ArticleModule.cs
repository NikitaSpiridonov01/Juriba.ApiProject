using System.Collections.Generic;
using Nancy;
using Juriba.ApiProject.Models;
using Juriba.ApiProject.Services;

namespace Juriba.ApiProject.Modules
{
    public sealed class ArticleModule : NancyModule
    {
        private readonly IArticleService _articleService;

        public ArticleModule(IArticleService articleService)
        {
            _articleService = articleService;

            Get("/", _ => Response.AsText("{\"status\": \"OK\"}", "application/json"));

            Get("/list/{section}", GetArticles);

            Get("/list/{section}/first", GetFirstArticle);

            Get("/list/{section}/{updatedDate}", GetArticlesSortedByUpdatedDate);

            Get("/article/{shortUrl}", GetArticleByShortUrl);

            Get("/group/{section}", GetArticlesGroupedByDate);
        }

        public object GetArticles(dynamic parameter)
        {
            IEnumerable<ArticleView> result = _articleService.GetSectionItems(parameter.section);

            return Response.AsJson(result);
        }

        public object GetFirstArticle(dynamic parameter)
        {
            ArticleView result = _articleService.GetFirstSectionItem(parameter.section);

            return Response.AsJson(result);
        }

        public object GetArticlesSortedByUpdatedDate(dynamic parameter)
        {
            IEnumerable<ArticleView> result = _articleService.GetSectionItemsSortedByUpdatedDate(parameter.section, parameter.updatedDate);

            return Response.AsJson(result);
        }

        public object GetArticleByShortUrl(dynamic parameter)
        {
            ArticleView result = _articleService.GetSectionItemByShortUrl("home", parameter.shortUrl);

            return Response.AsJson(result);
        }

        public object GetArticlesGroupedByDate(dynamic parameter)
        {
            IEnumerable<ArticleGroupedByDateView> result = _articleService.GetSectionItemsGroupedByDate(parameter.section);

            return Response.AsJson(result);
        }
    }
}
