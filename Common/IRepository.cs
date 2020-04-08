using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace DevopsGenie.Service.Common
{
    public interface IRepository
    {
        string CreateDocument(string db, string collection, JToken document);
    }
}