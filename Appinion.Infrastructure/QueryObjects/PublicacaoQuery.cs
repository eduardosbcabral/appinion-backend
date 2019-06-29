using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using Appinion.Infrastructure.QueryFilters;
using Appinion.Infrastructure.QueryModels;
using Appinion.Infrastructure.QueryObjects.Common;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appinion.Infrastructure.QueryObjects
{
    public class PublicacaoQuery : QueryObject, IQueryObject<PublicacaoQuery>
    {
        public PublicacaoQuery Query => this;

        private readonly IUsuarioRepository _usuarioRepository;

        public PublicacaoQuery(ISession session, IUsuarioRepository usuarioRepository)
            : base(session)
        {
            _usuarioRepository = usuarioRepository;
        }

        public object PesquisarPublicacoes(PesquisarPublicacoesQueryFilter filter)
        {
            if (string.IsNullOrEmpty(filter.Descritivo))
            {
                // Pesquisar sem filtro retorna lista vazia
                return new object();
            }
            else
            {
                var query = _session.QueryOver<Publicacao>()
                    .Where(x => x.Ativo)
                    .And(x => x.PublicacaoRespondida == null)
                    .And(x => x.PublicacaoRecompartilhada == null);

                var comparador = Restrictions.Disjunction();

                comparador.Add(Restrictions.InsensitiveLike(Projections.Property<Publicacao>(x => x.Conteudo), filter.Descritivo, MatchMode.Anywhere));

                query.Where(comparador);

                query.OrderBy(x => x.Data).Desc();

                var pagedObject = new PagedObject<Publicacao>();

                pagedObject.Paginate(query, 30, filter.Pagina);

                return pagedObject.PageResult(Mapper.Map<IList<PublicacaoQueryModelSearch>>(pagedObject.ResultQuery.List()));
            }
        }

        public object TimeLineUsuario(TimeLineUsuarioQueryFilter filter, int usuarioId)
        {
            var usuariosSeguindo = _session.QueryOver<SeguidorUsuario>()
                .Where(x => x.UsuarioSeguidor.Id == usuarioId)
                .Select(x => x.UsuarioSeguido.Id).List<int>();

            usuariosSeguindo.Add(usuarioId);

            var query = _session.QueryOver<Publicacao>()
                    .Where(x => x.Ativo)
                    .AndRestrictionOn(x => x.Usuario.Id).IsInG(usuariosSeguindo)
                    .OrderBy(x => x.Data).Desc;

            var pagedObject = new PagedObject<Publicacao>();

            pagedObject.Paginate(query, 30, filter.Pagina);

            return pagedObject.PageResult(Mapper.Map<IList<TimeLineQueryModel>>(pagedObject.ResultQuery.List()));
        }

        public object UsuarioPublicacoes(UsuarioPublicacoesQueryFilter filter)
        {
            var query = _session.QueryOver<Publicacao>()
                    .Where(x => x.Ativo)
                    .And(x => x.Usuario.Id == filter.Usuario.Id)
                    .OrderBy(x => x.Data).Desc;

            var pagedObject = new PagedObject<Publicacao>();

            pagedObject.Paginate(query, 30, filter.Pagina);

            return pagedObject.PageResult(Mapper.Map<IList<TimeLineQueryModel>>(pagedObject.ResultQuery.List()));
        }

        public DetalharPublicacaoQueryModel DetalharPublicacao(int publicacaoId, int usuarioId)
        {
            var query = _session.QueryOver<Publicacao>()
                .Where(x => x.Id == publicacaoId)
                .And(x => x.Ativo)
                .List()
                .FirstOrDefault();

            if (query == null)
            {
                return null;
            } 

            DetalharPublicacaoQueryModel detalharPublicacaoQueryModel = Mapper.Map<DetalharPublicacaoQueryModel>(query);

            if(query.UpVotes.Any())
                detalharPublicacaoQueryModel.UsuarioDeuUpvote = query.UpVotes.Where(x => x.Usuario.Id == usuarioId).Any();

            if(query.DownVotes.Any())
                detalharPublicacaoQueryModel.UsuarioDeuDownvote = query.DownVotes.Where(x => x.Usuario.Id == usuarioId).Any();

            return detalharPublicacaoQueryModel;
        }

        public IList<ComentariosPublicacaoQueryModel> ComentariosPublicacao(int publicacaoId)
        {
            var query = _session.QueryOver<Publicacao>()
                .Where(x => x.PublicacaoRespondida.Id == publicacaoId)
                .And(x => x.Ativo)
                .OrderBy(x => x.QuantidadeVotos).Desc
                .List();

            return Mapper.Map<IList<ComentariosPublicacaoQueryModel>>(query);
        }
    }
}
