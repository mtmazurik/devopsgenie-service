using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DevOpsGenieService.Common
{
    public class Repository : IRepository
    {
        private readonly string REPONOOK_URI_PORT = "http://localhost:8191";
        private HttpClient _client;

        public Repository(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> CreateDocumentAsync(string db, string collection, JToken document)
        {
            string apiResponse;
            //HttpContent body = new StringContent(JsonConvert.SerializeObject(document), Encoding.UTF8, "application/json");
            HttpContent body = new StringContent(document.ToString());
            string URI = REPONOOK_URI_PORT + "/" + db + "/" + collection;
            using (var response =  _client.PostAsync(URI, body).Result)
            {
                apiResponse = await response.Content.ReadAsStringAsync();
            }
            return apiResponse;
        }

    }
}
