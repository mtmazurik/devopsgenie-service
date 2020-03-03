using DevOpsGenieService.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevOpsGenieService.Tenant
{
    //
    // C.R.U. for Tenant Configuration
    //

    public class TenantConfigService : ITenantConfigService
    {
        private readonly string CONFIG_DB_NAME = "Tenant";
        private readonly string CONFIG_COLLECTION_NAME = "Config";

        private IRepository _repository;
        public TenantConfigService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateConfig(JToken configInfo)
        {
            return await _repository.CreateDocumentAsync(CONFIG_DB_NAME, CONFIG_COLLECTION_NAME, configInfo);
        }

        public async Task<string> ReadConfig()
        {
            return await Task.FromResult("NYI-not yet implemented");   // placeholder for async call to gRPC -> RepoNook
        }

    }
}
