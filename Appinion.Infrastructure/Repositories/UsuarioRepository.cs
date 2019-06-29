using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Repositories
{
    /// <summary>
    /// Classe que faz a implementação do Repositório para a entidade Usuário.
    /// </summary>
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ISession session)
            : base(session)
        {

        }

        public bool UsuarioExiste(string username)
        {
            return _session.QueryOver<Usuario>()
                .Where(x => x.Username == username)
                .And(x => x.Ativo)
                .RowCount() > 0;
        }

        public bool EmailExiste(string email)
        {
            return _session.QueryOver<Usuario>()
                .Where(x => x.Email == email)
                .And(x => x.Ativo)
                .RowCount() > 0;
        }

        public string RecuperarSenhaCriptografada(string username)
        {
            var query = _session.QueryOver<Usuario>()
                .Where(x => x.Username == username)
                .And(x => x.Ativo)
                .Select(x => x.Senha)
                .SingleOrDefault<string>();

            return query;
        }

        public Usuario RecuperarUsuarioPeloUsername(string username)
        {
            var query = _session.QueryOver<Usuario>()
                .Where(x => x.Username == username)
                .And(x => x.Ativo)
                .SingleOrDefault<Usuario>();

            return query;
        }
    }
}
