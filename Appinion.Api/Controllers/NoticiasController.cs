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
    [Route("noticias")]
    public class NoticiasController : ControllerBase
    {
        private readonly IQueryObject<NoticiaQuery> _noticiaQuery;

        private readonly IServiceContext _serviceContext;

        public NoticiasController(IQueryObject<NoticiaQuery> noticiaQuery, IServiceContext serviceContext)
        {
            _noticiaQuery = noticiaQuery;

            _serviceContext = serviceContext;
        }

        [HttpPost]
        [Route("listar-noticias")]
        public IActionResult ListarNoticiasParaOpinar([FromBody] ListarNoticiasParaOpinarQueryFilter filter)
        {
            var noticias = _noticiaQuery.Query.ListarNoticiasParaOpinar(filter, _serviceContext.UsuarioAtualId);

            return Ok(noticias);
        }

        [HttpPost]
        [Route("mais-concordadas")]
        public IActionResult ListarNoticiasMaisConcordadasDaSemana([FromBody] ListarNoticiasParaOpinarQueryFilter filter)
        {
            var noticias = _noticiaQuery.Query.ListarNoticiasMaisConcordadasDaSemana(filter);

            return Ok(noticias);
        }

        [HttpPost]
        [Route("mais-discordadas")]
        public IActionResult ListarNoticiasMaisDiscordadasDaSemana([FromBody] ListarNoticiasParaOpinarQueryFilter filter)
        {
            var noticias = _noticiaQuery.Query.ListarNoticiasMaisDiscordadasDaSemana(filter);

            return Ok(noticias);
        }
    }
}
