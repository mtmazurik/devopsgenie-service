using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOpsGenieService.Tenant
{
    //
    // C.R.U. for Tenant Configuration
    //
    public class TenantConfigService : ITenantConfigService
    {
        public async Task<string> ReadConfig()
        {
            return await Task.FromResult("NYI-not yet implemented");   // placeholder for async call to gRPC -> RepoNook
        }
        public async Task CreateConfig()
        {
            await Task.CompletedTask;       // noop placeholder
        }
    }
}
