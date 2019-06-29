using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using NHibernate;
using System.Linq;

namespace Appinion.Infrastructure.Repositories
{
    public class SeguidorUsuarioRepository : Repository<SeguidorUsuario>, ISeguidorUsuarioRepository
    {
        public SeguidorUsuarioRepository(ISession session) : base(session)
        {
        }
        
        public SeguidorUsuario UsuarioJaSeguido(Usuario usuarioSeguidor, Usuario usuarioSeguido)
        {
            return _session.QueryOver<SeguidorUsuario>()
                .Where(x => x.UsuarioSeguidor.Id == usuarioSeguidor.Id)
                .And(x => x.UsuarioSeguido.Id == usuarioSeguido.Id)
                .List()
                .FirstOrDefault();
        }
    }
}
