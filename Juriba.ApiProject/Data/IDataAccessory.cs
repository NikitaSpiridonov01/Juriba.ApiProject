using Newtonsoft.Json.Linq;

namespace Juriba.ApiProject.Data
{
    public interface IDataAccessory
    {
        JToken[] GetData(string section);
    }
}
