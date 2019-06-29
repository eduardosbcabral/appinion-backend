using Appinion.Domain.Entity;
using Appinion.Infrastructure.QueryModels;
using Appinion.Infrastructure.QueryObjects.Common;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Linq;

namespace Appinion.Infrastructure.QueryObjects
{
    public class TituloUsuarioQuery : QueryObject, IQueryObject<TituloUsuarioQuery>
    {
        public TituloUsuarioQuery Query { get { return this; } }

        public TituloUsuarioQuery(ISession session) : base(session)
        {
        }

        public ISession GetSession()
        {
            return _session;
        }

        public RecuperarUsuarioComMaisUpvotesNaSemanaQueryModel RecuperarUsuarioComMaisUpvotesNaSemana()
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            RecuperarUsuarioComMaisUpvotesNaSemanaQueryModel usuarioQueryModel = null;
            seteDiasAtras = new DateTime(2019, 03, 30);

            Usuario usuarioAlias = null;

            var query = _session.QueryOver<Publicacao>()
                .JoinAlias(x => x.Usuario, () => usuarioAlias)
                .Where(x => x.Data >= seteDiasAtras)
                .SelectList(l => l
                    .Select(x => usuarioAlias.Id).WithAlias(() => usuarioQueryModel.UsuarioId)
                    .SelectGroup(x => usuarioAlias.Id)
                    .SelectCount(x => x.UpVotes).WithAlias(() => usuarioQueryModel.QuantidadeUpvotes))
                .OrderByAlias(() => usuarioQueryModel.QuantidadeUpvotes).Desc
                .TransformUsing(Transformers.AliasToBean<RecuperarUsuarioComMaisUpvotesNaSemanaQueryModel>())
                .List<RecuperarUsuarioComMaisUpvotesNaSemanaQueryModel>()
                .FirstOrDefault();

            return query;
        }

        public RecuperarUsuarioComMaisDownvotesNaSemanaQueryModel RecuperarUsuarioComMaisDownvotesNaSemana()
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            RecuperarUsuarioComMaisDownvotesNaSemanaQueryModel usuarioQueryModel = null;
            seteDiasAtras = new DateTime(2019, 03, 30);

            Usuario usuarioAlias = null;

            var query = _session.QueryOver<Publicacao>()
                .JoinAlias(x => x.Usuario, () => usuarioAlias)
                .Where(x => x.Data >= seteDiasAtras)
                .SelectList(l => l
                    .Select(x => usuarioAlias.Id).WithAlias(() => usuarioQueryModel.UsuarioId)
                    .SelectGroup(x => usuarioAlias.Id)
                    .SelectCount(x => x.DownVotes).WithAlias(() => usuarioQueryModel.QuantidadeDownvotes))
                .OrderByAlias(() => usuarioQueryModel.QuantidadeDownvotes).Desc
                .TransformUsing(Transformers.AliasToBean<RecuperarUsuarioComMaisDownvotesNaSemanaQueryModel>())
                .List<RecuperarUsuarioComMaisDownvotesNaSemanaQueryModel>()
                .FirstOrDefault();

            return query;
        }

        public RecuperarUsuarioComMaisPublicacoesNaSemanaQueryModel RecuperarUsuarioComMaisPublicacoesNaSemana()
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            seteDiasAtras = new DateTime(2019, 5, 1);

            RecuperarUsuarioComMaisPublicacoesNaSemanaQueryModel usuarioQueryModel = null;

            Usuario usuarioAlias = null;

            var query = _session.QueryOver<Publicacao>()
                .JoinAlias(x => x.Usuario, () => usuarioAlias)
                .Where(x => x.Data >= seteDiasAtras)
                .SelectList(l => l
                    .Select(x => usuarioAlias.Id).WithAlias(() => usuarioQueryModel.UsuarioId)
                    .SelectGroup(x => usuarioAlias.Id)
                    .SelectCount(x => x.Id).WithAlias(() => usuarioQueryModel.QuantidadePublicacoes))
                .OrderByAlias(() => usuarioQueryModel.QuantidadePublicacoes).Desc
                .TransformUsing(Transformers.AliasToBean<RecuperarUsuarioComMaisPublicacoesNaSemanaQueryModel>())
                .List<RecuperarUsuarioComMaisPublicacoesNaSemanaQueryModel>()
                .FirstOrDefault();

            return query;
        }
    }
}
