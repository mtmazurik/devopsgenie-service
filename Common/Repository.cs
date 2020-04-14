using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections;
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

        public string CreateDocument(string db, string collection, string body)
        {
            string apiResponse;
            return body;
            ParseMetadataFromBody(body
                      , out string id
                      , out string key
                      , out string tags
                      , out string app
                      , out string document);


            RepositoryModel repoObject = new RepositoryModel();
            if (id == string.Empty)
            {
                repoObject._id = Guid.NewGuid();
            }
            repoObject.key = key;
            repoObject.tags = JsonConvert.DeserializeObject<string[]>(tags);
            repoObject.createdBy = "DOG-SVC";
            repoObject.createdDate = DateTime.Now;
            repoObject.modifiedBy = "DOG-SVC";
            repoObject.modifiedDate = DateTime.Now;
            repoObject.app = app;
            repoObject.repository = db;
            repoObject.collection = collection;
            repoObject.validate = false;
            repoObject.schemaUri = "";
            if (DoEncrypt == true)
            {
                repoObject.data = _encryption.encrypt(JsonConvert.SerializeObject(document));
            }
            else
            {
                repoObject.data = JsonConvert.SerializeObject(document);
            }

            HttpContent outBody = new StringContent(JsonConvert.SerializeObject(repoObject), Encoding.UTF8, "application/json");

            string uri = BuildURI();
            uri = uri + "/" + db + "/" + collection;
            // return _encryption.EncryptionKey ; // debug - see what secret value is
            HttpResponseMessage result = _client.SendAsync(FormatRequest(HttpMethod.Post, uri, outBody)).Result;

            apiResponse = result.Content.ReadAsStringAsync().Result;

            return apiResponse;
        }

        private void ParseMetadataFromBody(string body, out string id, out string key, out string tags, out string app, out string document)
        {
            try
            {
                id = string.Empty;
                key = string.Empty;
                tags = string.Empty;
                app = string.Empty;
                document = string.Empty;
                JObject data = JObject.Parse(body);
                id = (string)data["metadata"]["id"];
                key = (string)data["metadata"]["key"];
                tags = data["metadata"]["tags"].ToString();
                app = (string)data["metadata"]["app"];
                document = data["document"].ToString();
            }
            catch( Exception exc)
            {
                throw new APIBodyParseError(exc.Message);
            }
        }

        private Boolean DoEncrypt   // read-only property
        {
            get
            { 
                bool doEncrypt = _config.DO_ENCRYPT.Equals("true", StringComparison.OrdinalIgnoreCase);
                if (doEncrypt)
                {
                    _encryption.EncryptionKey = _config.ENCRYPTION_KEY;
                }

                return doEncrypt;
            }
        }
        private string BuildURI()
        {
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
