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
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private ICommandHandler<AutenticarUsuarioCommand> _autenticarUsuario;

        public AuthController(ICommandHandler<AutenticarUsuarioCommand> autenticarUsuario)
        {
            _autenticarUsuario = autenticarUsuario;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] AutenticarUsuarioCommand command)
        {
            _autenticarUsuario.Execute(command);
            var result = _autenticarUsuario.ResultCommand as AutenticarUsuarioResultCommand;
            return Ok(result);
        }
    }
}
