using Appinion.Domain.Entity;
using Appinion.Infrastructure.Common;
using Appinion.Infrastructure.QueryFilters;
using Appinion.Infrastructure.QueryModels;
using Appinion.Infrastructure.QueryObjects.Common;
using AutoMapper;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryObjects
{
    public class NoticiaQuery : QueryObject, IQueryObject<NoticiaQuery>
    {
        public NoticiaQuery Query => this;

        public NoticiaQuery(ISession session)
            : base(session)
        {
        }

        public object ListarNoticiasParaOpinar(ListarNoticiasParaOpinarQueryFilter filter, int usuarioId)
        {
            // Trazer todas as opiniões do usuário
            var subQuery = _session.QueryOver<Opiniao>()
                .Where(x => x.Usuario.Id == usuarioId)
                .Select(x => x.Noticia.Id)
                .List<int>();

            // Recuperar as notícias que não são as notícias da consulta acima
            var query = _session.QueryOver<Noticia>()
                .WhereRestrictionOn(x => x.Id).Not.IsInG(subQuery)
                .OrderBy(x => x.Data).Desc;

            var pagedObject = new PagedObject<Noticia>();

            pagedObject.Paginate(query, 10, filter.Pagina);

            return pagedObject.PageResult(Mapper.Map<IList<NoticiaQueryModel>>(pagedObject.ResultQuery.List()));

        }

        public object ListarNoticiasMaisConcordadasDaSemana(ListarNoticiasParaOpinarQueryFilter filter)
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            ListarNoticiasMaisConcordadasDaSemanaQueryModel noticiaQueryModel = null;
            Noticia noticiaAlias = null;
            Opiniao opiniaoAlias = null;

            var query = _session.QueryOver<Opiniao>(() => opiniaoAlias)
                .JoinAlias(() => opiniaoAlias.Noticia, () => noticiaAlias)
                .Where(x => x.Data >= seteDiasAtras)
                .And(x => x.Tipo == TipoOpiniao.Concordo)
                .SelectList(l => l
                    .Select(x => noticiaAlias.Id).WithAlias(() => noticiaQueryModel.Id)
                    .Select(x => noticiaAlias.Titulo).WithAlias(() => noticiaQueryModel.Titulo)
                    .Select(x => noticiaAlias.Url).WithAlias(() => noticiaQueryModel.Url)
                    .Select(x => noticiaAlias.UrlImagem).WithAlias(() => noticiaQueryModel.UrlImagem)
                    .Select(x => noticiaAlias.Data).WithAlias(() => noticiaQueryModel.Data)
                    .SelectCount(x => x.Id).WithAlias(() => noticiaQueryModel.Quantidade)
                    .SelectGroup(x => noticiaAlias.Id))
                .OrderByAlias(() => noticiaQueryModel.Quantidade).Desc
                .TransformUsing(Transformers.AliasToBean<ListarNoticiasMaisConcordadasDaSemanaQueryModel>());

            var pagedObject = new PagedObject<Opiniao>();

            pagedObject.Paginate(query, 10, filter.Pagina);

            return pagedObject.PageResult(Mapper.Map<IList<ListarNoticiasMaisConcordadasDaSemanaQueryModel>>(pagedObject.ResultQuery.List<ListarNoticiasMaisConcordadasDaSemanaQueryModel>()));
        }

        public object ListarNoticiasMaisDiscordadasDaSemana(ListarNoticiasParaOpinarQueryFilter filter)
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            ListarNoticiasMaisConcordadasDaSemanaQueryModel noticiaQueryModel = null;
            Noticia noticiaAlias = null;
            Opiniao opiniaoAlias = null;

            var query = _session.QueryOver<Opiniao>(() => opiniaoAlias)
                .JoinAlias(() => opiniaoAlias.Noticia, () => noticiaAlias)
                .Where(x => x.Data >= seteDiasAtras)
                .And(x => x.Tipo == TipoOpiniao.Discordo)
                .SelectList(l => l
                    .Select(x => noticiaAlias.Id).WithAlias(() => noticiaQueryModel.Id)
                    .Select(x => noticiaAlias.Titulo).WithAlias(() => noticiaQueryModel.Titulo)
                    .Select(x => noticiaAlias.Url).WithAlias(() => noticiaQueryModel.Url)
                    .Select(x => noticiaAlias.UrlImagem).WithAlias(() => noticiaQueryModel.UrlImagem)
                    .Select(x => noticiaAlias.Data).WithAlias(() => noticiaQueryModel.Data)
                    .SelectCount(x => x.Id).WithAlias(() => noticiaQueryModel.Quantidade)
                    .SelectGroup(x => noticiaAlias.Id))
                .OrderByAlias(() => noticiaQueryModel.Quantidade).Desc
                .TransformUsing(Transformers.AliasToBean<ListarNoticiasMaisConcordadasDaSemanaQueryModel>());

            var pagedObject = new PagedObject<Opiniao>();

            pagedObject.Paginate(query, 10, filter.Pagina);

            return pagedObject.PageResult(Mapper.Map<IList<ListarNoticiasMaisConcordadasDaSemanaQueryModel>>(pagedObject.ResultQuery.List<ListarNoticiasMaisConcordadasDaSemanaQueryModel>()));
        }
    }
}
