using DevopsGenie.Service.Common;
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

        public string CreateConfig(JToken configInfo)
        {
            return _repository.CreateDocument(CONFIG_DB_NAME, CONFIG_COLLECTION_NAME, configInfo);
        }

        public string ReadConfig()
        {
            return  "NYI-not yet implemented";   // placeholder for async call to gRPC -> RepoNook
        }

    }
}
