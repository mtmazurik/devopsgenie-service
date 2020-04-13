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
using devopsgenie.service.Config;
using devopsgenie.service.Common;
using DevopsGenie.Reponook.Exceptions;

namespace DevopsGenie.Service.Common
{
    // DAL that uses the RepositoryModel (an exact RepoNook model object, must match the Reponook model from DevopsGenie-Reponook service)
    public class Repository : IRepository
    {
        private string REPONOOK_URI;
        private HttpClient _client;
        private IJsonConfiguration _config;
        private IEncryption _encryption;

        public Repository(HttpClient client, IJsonConfiguration config, IEncryption encryption)
        {
            _client = client;
            _config = config;
            _encryption = encryption;
        }

        public string CreateDocument(string db, string collection, string document)
        {
            string apiResponse;

            RepositoryModel repoObject = new RepositoryModel();

            repoObject._id = Guid.NewGuid();
            repoObject.key = "key";
            repoObject.tags = new string[] { "tag1", "tag2"};
            repoObject.createdBy = "DOG-SVC";
            _encryption.EncryptionKey = repoObject.createdBy;
            repoObject.createdDate = DateTime.Now;
            repoObject.modifiedBy = "DOG-SVC";
            repoObject.modifiedDate = DateTime.Now;
            repoObject.app = "DOG";
            repoObject.repository = "tenant";
            repoObject.collection = "config";
            repoObject.validate = false;
            repoObject.schemaUri = "";
            repoObject.data = JsonConvert.SerializeObject(document); // _encryption.encrypt(document);

            HttpContent body = new StringContent(JsonConvert.SerializeObject(repoObject), Encoding.UTF8, "application/json");

            string uri = BuildURI();
            uri = uri + "/" + db + "/" + collection;
            //return uri;
            HttpResponseMessage result = _client.SendAsync(FormatRequest(HttpMethod.Post, uri, body)).Result;

            apiResponse = result.Content.ReadAsStringAsync().Result;

            return apiResponse;
        }
        private string BuildURI()
        {
            //byte[] data = Convert.FromBase64String(_config.DOGREPONOOK_URI);
            //string dogReponookURI = Encoding.UTF8.GetString(data);

            //data = Convert.FromBase64String(_config.DOGREPONOOK_PORT);
            //string dogReponookPort = Encoding.UTF8.GetString(data);

            return _config.DOGREPONOOK_URI + ":" + _config.DOGREPONOOK_PORT;
        }
        private HttpRequestMessage FormatRequest(HttpMethod method, string uri, HttpContent content = null)
        {
            HttpRequestMessage retRequest = new HttpRequestMessage()
            {
                RequestUri = new Uri(uri),
                Method = method
            };

            retRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            retRequest.Content = content;

            return retRequest;
        }

    }
}
