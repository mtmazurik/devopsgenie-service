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
        [HttpGet("ping")]   // ping
        public IActionResult GetPing()
        {
            return Ok("200 OK");
        }
        [HttpPost("{tenantId}/config")]
        public ActionResult<string> PostConfig(string tenantId, [FromBody]string body, [FromServices]ITenantConfigService tenantConfigSvc)
        {
            try
            {
                return Ok(body);
                //string result = tenantConfigSvc.CreateConfig(body);
                //return Ok(result);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        //GET: tenant/config
        //[HttpGet("{tenantId}/config")]
        //public ActionResult<string> GetConfig([FromServices]ITenantConfigService tenantConfigSvc)
        //{
        //    try
        //    {
        //        string result = tenantConfigSvc.ReadConfig();
        //        return Ok(result);
        //    }
        //    catch(Exception exc)
        //    {
        //        return BadRequest(exc.Message);
        //    }

        //}

    }
}
