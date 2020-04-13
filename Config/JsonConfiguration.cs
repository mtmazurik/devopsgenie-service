using DevopsGenie.Reponook.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace devopsgenie.service.Config
{
    public class JsonConfiguration : IJsonConfiguration
    {
        private IConfiguration _configuration;
        public JsonConfiguration()              // ctor
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();         //IMPORTANT!    allows reading out of the local (not uploaded) launchSettings.json for Docker
            _configuration = configBuilder.Build();
        }


        public string DOGREPONOOK_URI
        {
            get
            {
                string connectionString = _configuration["DOGREPONOOK_URI"];
                if (connectionString is null) throw new ConfigFileReadError("Check appsettings.json; ENV Var DOGREPONOOK_URI not found.");
                return connectionString;
            }
        }
        public string DOGREPONOOK_PORT
        {
            get
            {
                string port = _configuration["DOGREPONOOK_PORT"];
                if (port is null) throw new ConfigFileReadError("Check appsettings.json; ENV Var DOGREPONOOK_PORT not found.");
                return port;
            }
        }
        public string DO_ENCRYPT
        {
            get
            {
                string doEncrypt = _configuration["DO_ENCRYPT"];
                if (doEncrypt is null) throw new ConfigFileReadError("Check appsettings.json; ENV Var DO_ENCRYPT not found.");
                return doEncrypt;
            }
        }
        public string ENCRYPTION_KEY
        {
            get
            {
                string key = _configuration["ENCRYPTION_KEY"];
                if (key is null) throw new ConfigFileReadError("Check appsettings.json; ENV Var ENCRYPTION_KEY not found.");
                return key;
            }
        }
    }
}
