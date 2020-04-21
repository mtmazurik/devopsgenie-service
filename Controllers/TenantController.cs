using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevopsGenie.Service.Tenant;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using DevopsGenie.Service.Common.Models;

namespace DevopsGenie.Service.Controllers
{
    [ApiController]
    [Route("/")]
    public class TenantController : ControllerBase
    {
        // POST create config
        [HttpPost("/config")]
        public ActionResult<string> PostConfig([FromServices]ITenantConfigService tenantConfigSvc, string tenantId, [FromBody]JObject body) 
        {
            try
            {
                string result = tenantConfigSvc.CreateConfig(body.ToString());
                return Ok(result);
            }
            catch (Exception exc)
            {
                return Ok(exc.Message);
            }
        }

        // GET read config
        [HttpGet("/tenant/{tenantId}/config")]
        public ActionResult<string> GetConfig([FromServices]ITenantConfigService tenantConfigSvc, string tenantId)
        {
            try
            {
                string result = tenantConfigSvc.ReadConfig(tenantId);
                return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }

        }

    }
}
