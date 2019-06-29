using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using NHibernate;
using System.Linq;

namespace Appinion.Infrastructure.Repositories
{
    public class PublicacaoDownvoteRepository : Repository<PublicacaoDownvote>, IPublicacaoDownvoteRepository
    {
        public PublicacaoDownvoteRepository(ISession session)
            : base(session)
        {

        }

        public PublicacaoDownvote PodeDownvote(Publicacao publicacao, Usuario usuario)
        {
            return _session.QueryOver<PublicacaoDownvote>()
                .Where(x => x.Publicacao.Id == publicacao.Id)
                .And(x => x.Usuario.Id == usuario.Id)
                .List()
                .FirstOrDefault();
        }
    }
}