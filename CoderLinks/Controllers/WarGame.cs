using CoderLinks.BussinesLogic;
using CoderLinks.Modal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoderLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarGame : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            string result = string.Empty;
            try
            {
               return Helper.Instance.StartGame().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
