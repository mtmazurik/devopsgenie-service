using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using DevopsGenie.Service.Common.Models;
using System.Collections.Generic;

namespace DevopsGenie.Service.Common
{
    public interface IRepository
    {
        string CreateDocument(string db, string collection, string document);
        Task<List<RepositoryModel>> GetDocumentByKeyAndTag(string tenantId, string db, string collection, string key, string tag);
    }
}