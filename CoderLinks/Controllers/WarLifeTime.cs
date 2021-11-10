using CoderLinks.BussinesLogic;
using CoderLinks.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CoderLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarLifeTime : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WarLog> Get()
        {
            try
            {
                return Helper.Instance.GetLog();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
