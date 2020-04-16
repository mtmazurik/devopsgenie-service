using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using DevopsGenie.Service.Common.Models;

namespace DevopsGenie.Service.Common
{
    public interface IRepository
    {
        string CreateDocument(string db, string collection, string document);
        RepositoryModel GetDocument(string key, string tag);
    }
}