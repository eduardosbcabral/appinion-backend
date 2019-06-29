using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using Appinion.Infrastructure.QueryFilters;
using Appinion.Infrastructure.QueryModels;
using Appinion.Infrastructure.QueryObjects.Common;
using Appinion.Infrastructure.QueryObjects.UsuarioQueryObjects.QueryModels;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;

namespace Appinion.Infrastructure.Query
{
    public class UsuarioQuery : QueryObject, IQueryObject<UsuarioQuery>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioQuery Query => this;

        public UsuarioQuery(ISession session, IUsuarioRepository usuarioRepository)
            : base(session)
        {
            _usuarioRepository = usuarioRepository;
        }

        public object PesquisarUsuarios(PesquisarUsuariosQueryFilter filter)
        {
            if (string.IsNullOrEmpty(filter.Descritivo))
            {
                // Pesquisar sem filtro retorna lista vazia
                return new object();
            }
            else
            {
                var query = _session.QueryOver<Usuario>()
                    .Where(x => x.Ativo);

                var a = query.List();

                var comparador = Restrictions.Disjunction();

                comparador.Add(Restrictions.InsensitiveLike(Projections.Property<Usuario>(x => x.Username), filter.Descritivo, MatchMode.Start));
                comparador.Add(Restrictions.InsensitiveLike(Projections.Property<Usuario>(x => x.Nome), filter.Descritivo, MatchMode.Start));
                comparador.Add(Restrictions.InsensitiveLike(Projections.Property<Usuario>(x => x.Descricao), filter.Descritivo, MatchMode.Start));

                query.Where(comparador);

                query.OrderBy(x => x.Username).Asc();

                var pagedObject = new PagedObject<Usuario>();

                pagedObject.Paginate(query, 30, filter.Pagina);

                return pagedObject.PageResult(Mapper.Map<IList<UsuarioQueryModelSearch>>(pagedObject.ResultQuery.List()));
            }
        }

        public DetalharUsuarioQueryModel DetalharUsuarioSemLista(int id)
        {
            var query = _usuarioRepository.Find(id);
           
            return Mapper.Map<DetalharUsuarioQueryModel>(query);
        }

        public IList<SeguidorUsuarioQueryModelUsuarioSeguido> ListarSeguindo(int id)
        {
            var query = _usuarioRepository.Find(id).Seguindo;

            return Mapper.Map<IList<SeguidorUsuarioQueryModelUsuarioSeguido>>(query);
        }

        public IList<SeguidorUsuarioQueryModelUsuarioSeguidor> ListarSeguidores(int id)
        {
            var query = _usuarioRepository.Find(id).Seguidores;

            return Mapper.Map<IList<SeguidorUsuarioQueryModelUsuarioSeguidor>>(query);
        }

        public IList<UsuarioQueryModel> ListarTodos()
        {
            var query = _session.QueryOver<Usuario>()
                .List();

            return Mapper.Map<IList<UsuarioQueryModel>>(query);
        }

        public object ListarUsuariosMaisParecidosComUsuarioLogado(ListarUsuariosMaisParecidosComUsuarioLogadoQueryFilter filter, int usuarioLogadoId)
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            ListarUsuariosMaisParecidosComUsuarioLogadoQueryModel usuarioQueryModel = null;
            Usuario usuarioAlias = null;
            Arquivo fotoAlias = null;
            Opiniao opiniaoAlias = null;

            var noticias = _session.QueryOver<Opiniao>()
                .Where(x => x.Usuario.Id == usuarioLogadoId)
                .And(x => x.Tipo == TipoOpiniao.Concordo)
                .Select(x => x.Noticia.Id).List<int>();

            var query = _session.QueryOver<Opiniao>(() => opiniaoAlias)
                .JoinAlias(() => opiniaoAlias.Usuario, () => usuarioAlias)
                .Left.JoinAlias(() => usuarioAlias.Foto, () => fotoAlias)
                .Where(x => x.Tipo == TipoOpiniao.Concordo)
                .And(x => usuarioAlias.Id != usuarioLogadoId)
                .AndRestrictionOn(x => x.Noticia.Id).IsInG(noticias)
                .SelectList(l => l
                    .Select(x => usuarioAlias.Id).WithAlias(() => usuarioQueryModel.Id)
                    .Select(x => usuarioAlias.Username).WithAlias(() => usuarioQueryModel.Username)
                    .Select(x => usuarioAlias.Nome).WithAlias(() => usuarioQueryModel.Nome)
                    .Select(x => usuarioAlias.Email).WithAlias(() => usuarioQueryModel.Email)
                    .Select(x => fotoAlias.Nome).WithAlias(() => usuarioQueryModel.FotoNome)
                    .SelectCount(x => x.Id).WithAlias(() => usuarioQueryModel.Quantidade)
                    .SelectGroup(x => usuarioAlias.Id))
                .OrderByAlias(() => usuarioQueryModel.Quantidade).Desc
                .TransformUsing(Transformers.AliasToBean<ListarUsuariosMaisParecidosComUsuarioLogadoQueryModel>());

            var pagedObject = new PagedObject<Opiniao>();

            pagedObject.Paginate(query, 10, filter.Pagina);

            return pagedObject.PageResult(Mapper.Map<IList<ListarUsuariosMaisParecidosComUsuarioLogadoQueryModel>>(pagedObject.ResultQuery.List<ListarUsuariosMaisParecidosComUsuarioLogadoQueryModel>()));
        }

        public object ListarUsuariosMenosParecidosComUsuarioLogado(ListarUsuariosMaisParecidosComUsuarioLogadoQueryFilter filter, int usuarioLogadoId)
        {
            DateTime seteDiasAtras = DateTime.Now.AddDays(-7);

            ListarUsuariosMenosParecidosComUsuarioLogadoQueryModel usuarioQueryModel = null;
            Usuario usuarioAlias = null;
            Arquivo fotoAlias = null;
            Opiniao opiniaoAlias = null;

            var noticias = _session.QueryOver<Opiniao>()
                .Where(x => x.Usuario.Id == usuarioLogadoId)
                .And(x => x.Tipo == TipoOpiniao.Discordo)
                .Select(x => x.Noticia.Id).List<int>();

            var query = _session.QueryOver<Opiniao>(() => opiniaoAlias)
                .JoinAlias(() => opiniaoAlias.Usuario, () => usuarioAlias)
                .Left.JoinAlias(() => usuarioAlias.Foto, () => fotoAlias)
                .Where(x => x.Tipo == TipoOpiniao.Discordo)
                .And(x => usuarioAlias.Id != usuarioLogadoId)
                .AndRestrictionOn(x => x.Noticia.Id).IsInG(noticias)
                .SelectList(l => l
                    .Select(x => usuarioAlias.Id).WithAlias(() => usuarioQueryModel.Id)
                    .Select(x => usuarioAlias.Username).WithAlias(() => usuarioQueryModel.Username)
                    .Select(x => usuarioAlias.Nome).WithAlias(() => usuarioQueryModel.Nome)
                    .Select(x => usuarioAlias.Email).WithAlias(() => usuarioQueryModel.Email)
                    .Select(x => fotoAlias.Nome).WithAlias(() => usuarioQueryModel.FotoNome)
                    .SelectCount(x => x.Id).WithAlias(() => usuarioQueryModel.Quantidade)
                    .SelectGroup(x => usuarioAlias.Id))
                .OrderByAlias(() => usuarioQueryModel.Quantidade).Desc
                .TransformUsing(Transformers.AliasToBean<ListarUsuariosMenosParecidosComUsuarioLogadoQueryModel>());

            var pagedObject = new PagedObject<Opiniao>();

            pagedObject.Paginate(query, 10, filter.Pagina);

            return pagedObject.PageResult(Mapper.Map<IList<ListarUsuariosMenosParecidosComUsuarioLogadoQueryModel>>(pagedObject.ResultQuery.List<ListarUsuariosMenosParecidosComUsuarioLogadoQueryModel>()));
        }
    }
}
