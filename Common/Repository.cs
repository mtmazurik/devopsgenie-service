﻿using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DevopsGenie.Service.Common.Models;

namespace DevopsGenie.Service.Common
{
    // DAL that uses the RepositoryModel (an exact RepoNook model object, must match the Reponook model from DevopsGenie-Reponook service)
    public class Repository : IRepository
    {
        private readonly string REPONOOK_URI_PORT = "http://dogreponook:8191";
        private HttpClient _client;

        public Repository(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> CreateDocumentAsync(string db, string collection, JToken document)
        {
            string apiResponse;

            RepositoryModel repoObject = new RepositoryModel();

            repoObject._id = Guid.NewGuid();
            repoObject.key = "key";
            repoObject.tags = new string[] { "tag1", "tag2"};
            repoObject.createdBy = "DOG-SVC";
            repoObject.createdDate = DateTime.Now;
            repoObject.modifiedBy = "DOG-SVC";
            repoObject.modifiedDate = DateTime.Now;
            repoObject.app = "DOG";
            repoObject.repository = "tenant";
            repoObject.collection = "config";
            repoObject.validate = false;
            repoObject.schemaUri = "";
            repoObject.data = document.ToString();

            HttpContent body = new StringContent(JsonConvert.SerializeObject(repoObject), Encoding.UTF8, "application/json");


            string URI = REPONOOK_URI_PORT + "/" + db + "/" + collection;

            using (var response =  _client.PostAsync(URI, body ).Result)
            {
                apiResponse = await response.Content.ReadAsStringAsync();
            }
            return apiResponse;
        }

    }
}
