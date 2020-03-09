using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevopsGenie.Service.Tenant;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace DevopsGenie.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantController : ControllerBase
    {
        //GET: tenant/config
        [HttpGet("{tenantId}/config")]
        public ActionResult<string> GetConfig([FromServices]ITenantConfigService svc)
        {
            return Ok(svc.ReadConfig());
        }
        [HttpPost("{tenantId}/config")]
        public ActionResult<string> PostConfig([FromServices]ITenantConfigService svc, string tenantId, [FromBody]JToken body)
        {
            return Ok(svc.CreateConfig(body));
        }

        /*        // GET: Tenant
                [HttpGet]
                public IEnumerable<string> Get()
                {
                    return new string[] { "value1", "value2" };
                }

                // GET: Tenant/5
                [HttpGet("{id}", Name = "Get")]
                public string Get(int id)
                {
                    return "value";
                }

                // POST: Tenant
                [HttpPost]
                public void Post([FromBody] string value)
                {
                }

                // PUT: Tenant/5
                [HttpPut("{id}")]
                public void Put(int id, [FromBody] string value)
                {
                }

                // DELETE: ApiWithActions/5
                [HttpDelete("{id}")]
                public void Delete(int id)
                {
                }*/
    }
}
