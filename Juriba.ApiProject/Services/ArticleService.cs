using System;
using System.Collections.Generic;
using System.Linq;
using Juriba.ApiProject.Data;
using Juriba.ApiProject.Models;
using Newtonsoft.Json.Linq;

namespace Juriba.ApiProject.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IDataAccessory _dataAccessory;

        public ArticleService(IDataAccessory dataAccessory)
        {
            _dataAccessory = dataAccessory;
        }

        public IEnumerable<ArticleView> GetSectionItems(string section)
        {
            return _dataAccessory.GetData(section).Select(x => new ArticleView(x));
        }

        public ArticleView GetFirstSectionItem(string section)
        {
            return GetSectionItems(section).FirstOrDefault();
        }

        public IEnumerable<ArticleView> GetSectionItemsSortedByUpdatedDate(string section, string date)
        {
            return GetSectionItems(section).Where(x => x.Updated.ToString("yyyy-MM-dd") == date);
        }

        public ArticleView GetSectionItemByShortUrl(string section, string shortUrl)
        {
            JToken result = _dataAccessory.GetData(section)
                .FirstOrDefault(x => x
                .Value<string>("short_url")
                .Contains(shortUrl));

            if (result == null)
            {
                return null;
            }

            return new ArticleView(result);
        }

        public IEnumerable<ArticleGroupedByDateView> GetSectionItemsGroupedByDate(string section)
        {
            return _dataAccessory.GetData(section)
                .GroupBy(x => x
                .Value<DateTime>("created_date").Date
                .ToString("yyyy-MM-dd"))
                .Select(s => new ArticleGroupedByDateView
                {
                    Date = s.Key,
                    Total = s.Count()
                });
        }
    }
}
