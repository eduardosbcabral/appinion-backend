using Appinion.ApplicationService.CommandHandlers;
using Appinion.ApplicationService.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appinion.Api.Controllers
{
    [Authorize]
    [Route("opinioes")]
    public class OpinioesController : ControllerBase
    {
        private readonly ICommandHandler<ConcordarNoticiaCommand> _concordarNoticia;
        private readonly ICommandHandler<DiscordarNoticiaCommand> _discordarNoticia;

        public OpinioesController(
            ICommandHandler<ConcordarNoticiaCommand> concordarNoticia,
            ICommandHandler<DiscordarNoticiaCommand> discordarNoticia
            )
        {
            _concordarNoticia = concordarNoticia;
            _discordarNoticia = discordarNoticia;
        }

        [HttpPost]
        [Route("concordar")]
        public IActionResult ConcordarNoticia([FromBody] ConcordarNoticiaCommand command)
        {
            _concordarNoticia.Execute(command);
            return Ok();
        }

        [HttpPost]
        [Route("discordar")]
        public IActionResult DiscordarNoticia([FromBody] DiscordarNoticiaCommand command)
        {
            _discordarNoticia.Execute(command);
            return Ok();
        }
    }
}
