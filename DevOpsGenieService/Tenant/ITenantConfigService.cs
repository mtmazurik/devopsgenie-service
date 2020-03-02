using System.Threading.Tasks;

namespace DevOpsGenieService.Tenant
{
    public interface ITenantConfigService
    {
        Task CreateConfig();
        Task<string> ReadConfig();
    }
}