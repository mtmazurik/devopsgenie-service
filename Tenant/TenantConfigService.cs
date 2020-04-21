using DevopsGenie.Service.Common;
using DevopsGenie.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevopsGenie.Service.Tenant
{
    //
    // C.R.U. for Tenant Configuration
    //

    public class TenantConfigService : ITenantConfigService
    {
        private readonly string CONFIG_DB_NAME = "tenant";
        private readonly string CONFIG_COLLECTION_NAME = "config";

        private IRepository _repository;
        public TenantConfigService(IRepository repository)
        {
            _repository = repository;
        }

        public string CreateConfig(string body)
        {
            return _repository.CreateDocument(CONFIG_DB_NAME, CONFIG_COLLECTION_NAME, body);
        }

        public string ReadConfig(string tenantId)
        {
            const string key = "UI_CONFIG";
            const string tag = "type:imageRegistries";

            Task<List<RepositoryModel>> task = Task.Run<List<RepositoryModel>>(async () => await ReadConfigAsync(tenantId, key, tag));
            RepositoryModel repoObject = task.Result[0];
            return repoObject.data;
        }
        private async Task<List<RepositoryModel>> ReadConfigAsync(string tenantId, string key, string tag)
        {
            List<RepositoryModel> result = await _repository.GetDocumentByKeyAndTag(tenantId, CONFIG_DB_NAME, CONFIG_COLLECTION_NAME, key, tag);
            return result as List<RepositoryModel>;
        }

    }
}
