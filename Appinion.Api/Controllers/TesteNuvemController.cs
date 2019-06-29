using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appinion.Api.Controllers
{
    [AllowAnonymous]
    public class TesteNuvemController : ControllerBase
    {
        [Route("teste-cloud")]
        public IActionResult TesteNuvem()
        {
            return Ok("A API está na cloud!");
        }
    }
}
