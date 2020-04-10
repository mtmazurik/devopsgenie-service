using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevopsGenie.Service.Tenant;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using devopsgenie.service.Common.Models;

namespace DevopsGenie.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantController : ControllerBase
    {
        [HttpPost("{tenantId}/config")]
        public ActionResult<string> PostConfig([FromServices]ITenantConfigService tenantConfigSvc, string tenantId, [FromBody]InnerDataModel innerData) 
        {
            try
            {
                return Ok(innerData.Body);
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
