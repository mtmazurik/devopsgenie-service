using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections;
using DevopsGenie.Service.Common.Models;
using devopsgenie.service.Config;
using devopsgenie.service.Common;
using DevopsGenie.Reponook.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using devopsgenie.service.Common.Models;

namespace DevopsGenie.Service.Common
{
    // DAL that uses the RepositoryModel (an exact RepoNook model object, must match the Reponook model from DevopsGenie-Reponook service)
    public class Repository : IRepository
    {
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

            ParseMetadataFromBody(body
                      , out string id
                      , out string key
                      , out IEnumerable<string> tags
                      , out string app
                      , out string document);


            RepositoryModel repoObject = new RepositoryModel();
            if (id == string.Empty)
            {
                repoObject._id = Guid.NewGuid();
            }
            repoObject.key = key;
            repoObject.tags = tags;
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

            HttpResponseMessage result = _client.SendAsync(FormatRequest(HttpMethod.Post, uri, outBody)).Result;

            apiResponse = result.Content.ReadAsStringAsync().Result;

            return apiResponse;
        }

        private void ParseMetadataFromBody(string body, out string id, out string key, out IEnumerable<string> tags, out string app, out string document)
        {
            id = string.Empty;
            key = string.Empty;
            tags = null;
            app = string.Empty;
            document = string.Empty;
            JObject data = JObject.Parse(body);

            id = (string)data["metadata"]["id"];
            key = (string)data["metadata"]["key"];
            TranslateTags(body, ref tags);
            app = (string)data["metadata"]["app"];
            document = data["document"].ToString();
        }
        // TranslateTags: routine plucks from JSON and puts in C# array;
        // to pass to the other service (which puts back into JSON ... should we just use a simple string for tags?
        // maybe not, if the back end service will use tags as indexes eventually? the separation might be good here
        private void TranslateTags(string body, ref IEnumerable<string> tags)
        {
            var resultObjects = AllChildren(JObject.Parse(body))
                                   .First(c => c.Type == JTokenType.Array && c.Path.Contains("tags"))
                                   .Children<JObject>();

            List<string> list = new List<string>();
            foreach (JObject result in resultObjects)
            {
                foreach (JProperty property in result.Properties())
                {
                    string tag = "property.Name" + ":" + property.Value.ToString();
                    list.Add(tag);
                }
            }
            tags = list;
        }
        // recursively yield all children of json
        private IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
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
