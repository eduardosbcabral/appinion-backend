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
    public class TituloNoticiaMaisDiscordada : IJob
    {
        private readonly IQueryObject<TituloNoticiaQuery> _tituloNoticiaQuery;
        private readonly INoticiaRepository _noticiaRepository;
        private readonly ITituloNoticiaRepository _tituloNoticiaRepository;

        public TituloNoticiaMaisDiscordada(IQueryObject<TituloNoticiaQuery> tituloNoticiaQuery, INoticiaRepository noticiaRepository, ITituloNoticiaRepository tituloNoticiaRepository)
        {
            _tituloNoticiaQuery = tituloNoticiaQuery;
            _noticiaRepository = noticiaRepository;
            _tituloNoticiaRepository = tituloNoticiaRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            string hqlUpdate = "update tb_titulo t set t.atv_titulo = false where t.atv_titulo = true";
            _tituloNoticiaQuery.Query.GetSession().CreateQuery(hqlUpdate)
                .ExecuteUpdate();

            RecuperarNoticiaMaisDiscordadaQueryModel noticiaQueryModel = _tituloNoticiaQuery.Query.RecuperarNoticiaMaisDiscordada();
            Noticia noticia = _noticiaRepository.Find(noticiaQueryModel.NoticiaId);

            var titulo = new TituloNoticia(Titulos.NOTICIA_MAIS_DISCORDADA.ToString(), noticiaQueryModel.Quantidade, noticia);

            _tituloNoticiaRepository.Save(titulo);

            return Task.CompletedTask;
        }
    }
}