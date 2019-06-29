using Appinion.Domain.Entity;
using Appinion.Infrastructure.QueryModels;
using Appinion.Infrastructure.QueryObjects.Common;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appinion.Infrastructure.QueryObjects
{
    public class TituloNoticiaQuery : QueryObject, IQueryObject<TituloNoticiaQuery>
    {
        public TituloNoticiaQuery Query { get { return this; } }

        public TituloNoticiaQuery(ISession session) : base(session)
        {
        }

        public ISession GetSession()
        {
            return _session;
        }

        public RecuperarNoticiaMaisConcordadaQueryModel RecuperarNoticiaMaisConcordada()
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            RecuperarNoticiaMaisConcordadaQueryModel noticiaQueryModel = null;

            Noticia noticiaAlias = null;

            var query = _session.QueryOver<Opiniao>()
                .JoinAlias(x => x.Noticia, () => noticiaAlias)
                .Where(x => x.Data >= seteDiasAtras)
                .SelectList(l => l
                    .Select(x => x.Id).WithAlias(() => noticiaQueryModel.NoticiaId)
                    .SelectGroup(x => noticiaAlias.Id)
                    .SelectCount(x => x.Tipo == TipoOpiniao.Concordo).WithAlias(() => noticiaQueryModel.Quantidade))
                .OrderByAlias(() => noticiaQueryModel.Quantidade).Desc
                .TransformUsing(Transformers.AliasToBean<RecuperarNoticiaMaisConcordadaQueryModel>())
                .List<RecuperarNoticiaMaisConcordadaQueryModel>()
                .FirstOrDefault();

            return query;
        }

        public RecuperarNoticiaMaisDiscordadaQueryModel RecuperarNoticiaMaisDiscordada()
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            RecuperarNoticiaMaisDiscordadaQueryModel noticiaQueryModel = null;

            Noticia noticiaAlias = null;

            var query = _session.QueryOver<Opiniao>()
                .JoinAlias(x => x.Noticia, () => noticiaAlias)
                .Where(x => x.Data >= seteDiasAtras)
                .SelectList(l => l
                    .Select(x => x.Id).WithAlias(() => noticiaQueryModel.NoticiaId)
                    .SelectGroup(x => noticiaAlias.Id)
                    .SelectCount(x => x.Tipo == TipoOpiniao.Discordo).WithAlias(() => noticiaQueryModel.Quantidade))
                .OrderByAlias(() => noticiaQueryModel.Quantidade).Desc
                .TransformUsing(Transformers.AliasToBean<RecuperarNoticiaMaisDiscordadaQueryModel>())
                .List<RecuperarNoticiaMaisDiscordadaQueryModel>()
                .FirstOrDefault();

            return query;
        }
    }
}
