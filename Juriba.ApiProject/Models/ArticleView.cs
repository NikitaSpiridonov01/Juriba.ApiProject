using Newtonsoft.Json.Linq;
using System;

namespace Juriba.ApiProject.Models
{
    public class ArticleView
    {
        public string Heading { get; set; }

        public DateTime Updated { get; set; }

        public string Link { get; set; }

        public ArticleView()
        {
        }

        public ArticleView(JToken jToken)
        {
            Heading = jToken.Value<string>("title");
            Link = jToken.Value<string>("url");
            Updated = jToken.Value<DateTime>("updated_date");
        }
    }
}
