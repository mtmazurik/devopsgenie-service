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
        public ActionResult<string> GetConfig([FromServices]ITenantConfigService tenantConfigSvc)
        {
            try
            {
                string result = tenantConfigSvc.ReadConfig();
                return Ok(result);
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }

        }
        //[HttpPost("{tenantId}/config")]
        //public ActionResult<string> PostConfig([FromServices]ITenantConfigService svc, string tenantId, [FromBody]JToken body)
        //{

        //    return Ok(svc.CreateConfig(body));
        //}
    }
}
