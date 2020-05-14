using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Juriba.ApiProject.Data
{
    public class DataAccessory : IDataAccessory
    {
        public JToken[] GetData(string section)
        {
            string result = null;

            try
            {
                using (WebClient client = new WebClient())
                {
                    result = client.DownloadString(String.Format("https://api.nytimes.com/svc/topstories/v2/{0}.json?api-key={1}", section, "k0XA0k0jJGAVuv8Jr5wAIcKDGPuznmRJ"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (String.IsNullOrEmpty(result))
            {
                return new JToken[0];
            }
            else
            {
                return JObject.Parse(result).GetValue("results").ToObject<JToken[]>();
            }
        }
    }
}
