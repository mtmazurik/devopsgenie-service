using DevOpsGenieService.Common;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace DevOpsGenieService.Tenant
{
    public interface ITenantConfigService
    {
        Task<string> CreateConfig(JToken s);
        Task<string> ReadConfig();
    }
}