using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Core;
using Core.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private IReclameBlockService reclameBlockService { get; set; }
        public CostController(IReclameBlockService reclameBlockService)
        {
            this.reclameBlockService = reclameBlockService;
        }

        [HttpGet]
        public decimal GetCost(string sId)
        {
            try
            {
                long id = long.Parse(sId);
                return reclameBlockService.GetCostReleases(id);
            }
            catch(Exception ex)
            {
                return -2;
            }
        }

        [HttpGet]
        public string Test (){
            return "hello world";
        }
    }
}