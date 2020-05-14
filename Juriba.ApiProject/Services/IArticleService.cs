using System.Collections.Generic;
using Juriba.ApiProject.Models;

namespace Juriba.ApiProject.Services
{
    public interface IArticleService
    {
        IEnumerable<ArticleView> GetSectionItems(string section);

        ArticleView GetFirstSectionItem(string section);

        IEnumerable<ArticleView> GetSectionItemsSortedByUpdatedDate(string section, string date);

        ArticleView GetSectionItemByShortUrl(string section, string shortUrl);

        IEnumerable<ArticleGroupedByDateView> GetSectionItemsGroupedByDate(string section);
    }
}
