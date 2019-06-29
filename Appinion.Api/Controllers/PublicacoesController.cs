using Appinion.ApplicationService.CommandHandlers;
using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.Infrastructure.QueryFilters;
using Appinion.Infrastructure.QueryObjects;
using Appinion.Infrastructure.QueryObjects.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appinion.Api.Controllers
{
    [Authorize]
    [Route("publicacoes")]
    public class PublicacoesController : ControllerBase
    {
        private readonly IQueryObject<PublicacaoQuery> _publicacaoQuery;

        private readonly ICommandHandler<CadastrarPublicacaoCommand> _cadastrarPublicacao;
        private readonly ICommandHandler<InativarPublicacaoCommand> _inativarPublicacao;
        private readonly ICommandHandler<ComentarPublicacaoCommand> _comentarPublicacao;
        private readonly ICommandHandler<RecompartilharPublicacaoCommand> _recompartilharPublicacao;
        private readonly ICommandHandler<UpvotePublicacaoCommand> _upvotePublicacao;
        private readonly ICommandHandler<DownvotePublicacaoCommand> _downvotePublicacao;

        private readonly IServiceContext _serviceContext;

        public PublicacoesController(
            IQueryObject<PublicacaoQuery> publicacaoQuery,
            ICommandHandler<CadastrarPublicacaoCommand> cadastrarPublicacao,
            ICommandHandler<InativarPublicacaoCommand> inativarPublicacao,
            ICommandHandler<ComentarPublicacaoCommand> comentarPublicacao,
            ICommandHandler<RecompartilharPublicacaoCommand> recompartilharPublicacao,
            ICommandHandler<UpvotePublicacaoCommand> upvotePublicacao,
            ICommandHandler<DownvotePublicacaoCommand> downvotePublicacao,
            IServiceContext serviceContext)
        {
            _publicacaoQuery = publicacaoQuery;
            
            _cadastrarPublicacao = cadastrarPublicacao;
            _inativarPublicacao = inativarPublicacao;
            _comentarPublicacao = comentarPublicacao;
            _recompartilharPublicacao = recompartilharPublicacao;
            _upvotePublicacao = upvotePublicacao;
            _downvotePublicacao = downvotePublicacao;

            _serviceContext = serviceContext;
        }


        [Route("detalhar")]
        [HttpGet]
        public IActionResult DetalharPublicacao(int publicacaoId)
        {
            var query = _publicacaoQuery.Query.DetalharPublicacao(publicacaoId, _serviceContext.UsuarioAtualId);

            return Ok(query);
        }

        [Route("comentarios/{publicacaoId}")]
        [HttpGet]
        public IActionResult ComentariosPublicacao(int publicacaoId)
        {
            var query = _publicacaoQuery.Query.ComentariosPublicacao(publicacaoId);

            return Ok(query);
        }

        [Route("pesquisar")]
        [HttpPost]
        public IActionResult PesquisarPublicacoes([FromBody] PesquisarPublicacoesQueryFilter filter)
        {
            var query = _publicacaoQuery.Query.PesquisarPublicacoes(filter);

            return Ok(query);
        }

        [HttpPost]
        public IActionResult CadastrarPublicacao([FromForm] CadastrarPublicacaoCommand command)
        {
            _cadastrarPublicacao.Execute(command);
            return Ok();
        }

        [Route("comentar")]
        [HttpPost]
        public IActionResult ComentarPublicacao([FromForm] ComentarPublicacaoCommand command)
        {
            _comentarPublicacao.Execute(command);
            return Ok();
        }

        [Route("recompartilhar")]
        [HttpPost]
        public IActionResult RecompartilharPublicacao([FromForm] RecompartilharPublicacaoCommand command)
        {
            _recompartilharPublicacao.Execute(command);
            return Ok();
        }

        [Route("upvote")]
        [HttpPost]
        public IActionResult UpvotePublicacao([FromBody] UpvotePublicacaoCommand command)
        {
            _upvotePublicacao.Execute(command);
            return Ok();
        }

        [Route("downvote")]
        [HttpPost]
        public IActionResult DownvotePublicacao([FromBody] DownvotePublicacaoCommand command)
        {
            _downvotePublicacao.Execute(command);
            return Ok();
        }

        [Route("timeline")]
        [HttpPost]
        public IActionResult TimeLineUsuario([FromBody] TimeLineUsuarioQueryFilter filter)
        {
            var query = _publicacaoQuery.Query.TimeLineUsuario(filter, _serviceContext.UsuarioAtualId);

            return Ok(query);
        }

        [Route("usuario")]
        [HttpPost]
        public IActionResult UsuarioPublicacoes([FromBody] UsuarioPublicacoesQueryFilter filter)
        {
            var query = _publicacaoQuery.Query.UsuarioPublicacoes(filter);

            return Ok(query);
        }

        [Route("inativar")]
        [HttpDelete]
        public IActionResult InativarPublicacao([FromBody] InativarPublicacaoCommand command)
        {
            _inativarPublicacao.Execute(command);
            return Ok();
        }
    }
}
