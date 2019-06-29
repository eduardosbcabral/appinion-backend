using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Repositories
{
    public class TituloUsuarioRepository : Repository<TituloUsuario>, ITituloUsuarioRepository
    {
        public TituloUsuarioRepository(ISession session) : base(session)
        {
        }
    }
}
