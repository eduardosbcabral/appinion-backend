using Appinion.ApplicationService.CommandHandlers;
using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.Infrastructure.Query;
using Appinion.Infrastructure.QueryFilters;
using Appinion.Infrastructure.QueryObjects.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appinion.Api.Controllers
{
    [Authorize]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private IQueryObject<UsuarioQuery> _usuarioQuery;

        private ICommandHandler<CadastrarUsuarioCommand> _cadastrarUsuario;
        private ICommandHandler<AtualizarDescritivoUsuarioCommand> _atualizarDescritivoUsuario;
        private ICommandHandler<AtualizarUsernameUsuarioCommand> _atualizarUsernameUsuario;
        private ICommandHandler<AtualizarEmailUsuarioCommand> _atualizarEmailUsuario;
        private ICommandHandler<AtualizarFotoUsuarioCommand> _atualizarFotoUsuario;
        private ICommandHandler<AtualizarSenhaUsuarioCommand> _atualizarSenhaUsuario;
        private ICommandHandler<InativarUsuarioCommand> _inativarUsuario;
        private ICommandHandler<SeguirUsuarioCommand> _seguirUsuario;
        private ICommandHandler<PararSeguirUsuarioCommand> _pararSeguirUsuario;
        private IServiceContext _serviceContext;

        public UsuariosController(
            IQueryObject<UsuarioQuery> usuarioQuery,
            ICommandHandler<CadastrarUsuarioCommand> cadastrarUsuario,
            ICommandHandler<AtualizarDescritivoUsuarioCommand> atualizarDescritivoUsuario,
            ICommandHandler<AtualizarUsernameUsuarioCommand> atualizarUsernameUsuario,
            ICommandHandler<AtualizarEmailUsuarioCommand> atualizarEmailUsuario,
            ICommandHandler<AtualizarFotoUsuarioCommand> atualizarFotoUsuario,
            ICommandHandler<AtualizarSenhaUsuarioCommand> atualizarSenhaUsuario,
            ICommandHandler<InativarUsuarioCommand> inativarUsuario,
            ICommandHandler<SeguirUsuarioCommand> seguirUsuario,
            ICommandHandler<PararSeguirUsuarioCommand> pararSeguirUsuario,
            IServiceContext serviceContext
            )
        {
            _usuarioQuery = usuarioQuery;

            _cadastrarUsuario = cadastrarUsuario;
            _atualizarDescritivoUsuario = atualizarDescritivoUsuario;
            _atualizarUsernameUsuario = atualizarUsernameUsuario;
            _atualizarEmailUsuario = atualizarEmailUsuario;
            _atualizarFotoUsuario = atualizarFotoUsuario;
            _atualizarSenhaUsuario = atualizarSenhaUsuario;
            _inativarUsuario = inativarUsuario;
            _seguirUsuario = seguirUsuario;
            _pararSeguirUsuario = pararSeguirUsuario;

            _serviceContext = serviceContext;
        }

        // GET ROUTES
        [Route("listar-todos")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            var query = _usuarioQuery.Query.ListarTodos();

            return Ok(query);
        }

        [Route("detalhar-sem-lista")]
        [HttpGet]
        public IActionResult DetalharUsuarioSemLista(int usuarioId)
        {
            var query = _usuarioQuery.Query.DetalharUsuarioSemLista(usuarioId);

            return Ok(query);
        }

        [Route("pesquisar")]
        [HttpPost]
        public IActionResult PesquisarUsuarios([FromBody] PesquisarUsuariosQueryFilter filter)
        {
            var query = _usuarioQuery.Query.PesquisarUsuarios(filter);

            return Ok(query);
        }

        [Route("listar-seguindo")]
        [HttpGet]
        public IActionResult ListarSeguindo(int usuarioId)
        {
            var query = _usuarioQuery.Query.ListarSeguindo(usuarioId);

            return Ok(query);
        }

        [Route("listar-seguidores")]
        [HttpGet]
        public IActionResult ListarSeguidores(int usuarioId)
        {
            var query = _usuarioQuery.Query.ListarSeguidores(usuarioId);

            return Ok(query);
        }

        // POST ROUTES
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Cadastrar([FromBody] CadastrarUsuarioCommand command)
        {
            _cadastrarUsuario.Execute(command);
            return Ok();
        }

        // PATCH ROUTES
        [Route("atualizar-descritivo")]
        [HttpPatch]
        public IActionResult AtualizarDescritivo([FromBody] AtualizarDescritivoUsuarioCommand command)
        {
            _atualizarDescritivoUsuario.Execute(command);
            return Ok();
        }

        [Route("atualizar-username")]
        [HttpPatch]
        public IActionResult AtualizarUsername([FromBody] AtualizarUsernameUsuarioCommand command)
        {
            _atualizarUsernameUsuario.Execute(command);
            return Ok();
        }

        [Route("atualizar-email")]
        [HttpPatch]
        public IActionResult AtualizarEmail([FromBody] AtualizarEmailUsuarioCommand command)
        {
            _atualizarEmailUsuario.Execute(command);
            return Ok();
        }

        [Route("atualizar-foto")]
        [HttpPatch]
        public IActionResult AtualizarFoto([FromForm] AtualizarFotoUsuarioCommand command)
        {
            _atualizarFotoUsuario.Execute(command);
            return Ok();
        }

        [Route("atualizar-senha")]
        [HttpPatch]
        public IActionResult AtualizarSenha([FromBody] AtualizarSenhaUsuarioCommand command)
        {
            _atualizarSenhaUsuario.Execute(command);
            return Ok();
        }

        [Route("inativar")]
        [HttpDelete]
        public IActionResult InativarUsuario([FromBody] InativarUsuarioCommand command)
        {
            _inativarUsuario.Execute(command);
            return Ok();
        }

        [Route("seguir")]
        [HttpPost]
        public IActionResult SeguirUsuario([FromBody] SeguirUsuarioCommand command)
        {
            _seguirUsuario.Execute(command);
            return Ok();
        }

        [Route("parar-seguir")]
        [HttpPost]
        public IActionResult PararSeguirUsuario([FromBody] PararSeguirUsuarioCommand command)
        {
            _pararSeguirUsuario.Execute(command);
            return Ok();
        }

        [Route("mais-parecidos")]
        [HttpPost]
        public IActionResult ListarUsuariosMaisParecidos([FromBody] ListarUsuariosMaisParecidosComUsuarioLogadoQueryFilter filter)
        {
            return Ok(_usuarioQuery.Query.ListarUsuariosMaisParecidosComUsuarioLogado(filter, _serviceContext.UsuarioAtualId));
        }

        [Route("menos-parecidos")]
        [HttpPost]
        public IActionResult ListarUsuariosMenosParecidos([FromBody] ListarUsuariosMaisParecidosComUsuarioLogadoQueryFilter filter)
        {
            return Ok(_usuarioQuery.Query.ListarUsuariosMenosParecidosComUsuarioLogado(filter, _serviceContext.UsuarioAtualId));
        }
    }

}
