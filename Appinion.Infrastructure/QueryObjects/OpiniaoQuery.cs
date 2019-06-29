using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.QueryObjects.Common;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryObjects
{
    public class OpiniaoQuery : QueryObject, IQueryObject<OpiniaoQuery>
    {
        public OpiniaoQuery Query => this;

        private readonly IOpiniaoRepository _opiniaoRepository;

        public OpiniaoQuery(ISession session, IOpiniaoRepository opiniaoRepository)
            : base(session)
        {
            _opiniaoRepository = opiniaoRepository;
        }

        public IList<Opiniao> ListarOpinioesDiferentesDoUsuarioLogado(int usuarioId)
        {
            var query = _session.QueryOver<Opiniao>()
                .Where(x => x.Usuario.Id != usuarioId)
                .Take(100)
                .List();

            return query;
        }
    }
}
