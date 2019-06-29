using Appinion.Domain.Entity;
using Appinion.Domain.Enum;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.QueryModels;
using Appinion.Infrastructure.QueryObjects;
using Appinion.Infrastructure.QueryObjects.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Appinion.Infrastructure.Jobs
{
    public class TituloUsuarioComMaisUpvotesJob : IJob
    {
        private readonly IQueryObject<TituloUsuarioQuery> _tituloUsuarioQuery;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITituloUsuarioRepository _tituloUsuarioRepository;

        public TituloUsuarioComMaisUpvotesJob(IQueryObject<TituloUsuarioQuery> tituloUsuarioQuery, IUsuarioRepository usuarioRepository, ITituloUsuarioRepository tituloUsuarioRepository)
        {
            _tituloUsuarioQuery = tituloUsuarioQuery;
            _usuarioRepository = usuarioRepository;
            _tituloUsuarioRepository = tituloUsuarioRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            string hqlUpdate = "update tb_titulo t set t.atv_titulo = false where t.atv_titulo = true";
            _tituloUsuarioQuery.Query.GetSession().CreateQuery(hqlUpdate)
                .ExecuteUpdate();

            RecuperarUsuarioComMaisUpvotesNaSemanaQueryModel usuarioQueryModel = _tituloUsuarioQuery.Query.RecuperarUsuarioComMaisUpvotesNaSemana();
            Usuario usuario = _usuarioRepository.Find(usuarioQueryModel.UsuarioId);

            var titulo = new TituloUsuario(Titulos.USUARIO_COM_MAIOR_QUANTIDADE_DE_UPVOTES.ToString(), usuarioQueryModel.QuantidadeUpvotes, usuario);

            _tituloUsuarioRepository.Save(titulo);

            return Task.CompletedTask;
        }
    }
}
