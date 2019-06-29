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
    [Route("titulos")]
    public class TituloController : ControllerBase
    {
        private readonly IQueryObject<TituloUsuarioQuery> _tituloUsuarioQuery;
        private readonly IQueryObject<TituloNoticiaQuery> _tituloNoticiaQuery;

        public TituloController(IQueryObject<TituloUsuarioQuery> tituloUsuarioQuery, IQueryObject<TituloNoticiaQuery> tituloNoticiaQuery)
        {
            _tituloUsuarioQuery = tituloUsuarioQuery;
            _tituloNoticiaQuery = tituloNoticiaQuery;
        }

        [HttpGet]
        [Route("usuario-com-mais-publicacoes-semana")]
        public IActionResult RecuperarUsuarioComMaisPublicacoesNaSemana()
        {
            return Ok(_tituloUsuarioQuery.Query.RecuperarUsuarioComMaisPublicacoesNaSemana());
        }

        [HttpGet]
        [Route("usuario-com-mais-upvotes-semana")]
        public IActionResult RecuperarUsuarioComMaisUpvotesNaSemana()
        {
            return Ok(_tituloUsuarioQuery.Query.RecuperarUsuarioComMaisUpvotesNaSemana());
        }

        [HttpGet]
        [Route("usuario-com-mais-downvotes-semana")]
        public IActionResult RecuperarUsuarioComMaisDownvotesNaSemana()
        {
            return Ok(_tituloUsuarioQuery.Query.RecuperarUsuarioComMaisDownvotesNaSemana());
        }

        [HttpGet]
        [Route("noticia-mais-concordada")]
        public IActionResult RecuperarNoticiaMaisConcordada()
        {
            return Ok(_tituloNoticiaQuery.Query.RecuperarNoticiaMaisConcordada());
        }

        [HttpGet]
        [Route("noticia-mais-discordada")]
        public IActionResult RecuperarNoticiaMaisDiscordada()
        {
            return Ok(_tituloNoticiaQuery.Query.RecuperarNoticiaMaisDiscordada());
        }
    }
}
