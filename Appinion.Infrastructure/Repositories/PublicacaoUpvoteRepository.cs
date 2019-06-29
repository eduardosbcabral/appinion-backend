using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using NHibernate;
using System.Linq;

namespace Appinion.Infrastructure.Repositories
{
    public class PublicacaoUpvoteRepository : Repository<PublicacaoUpvote>, IPublicacaoUpvoteRepository
    {
        public PublicacaoUpvoteRepository(ISession session)
            : base(session)
        {

        }

        public PublicacaoUpvote PodeUpvote(Publicacao publicacao, Usuario usuario)
        {
            return _session.QueryOver<PublicacaoUpvote>()
                .Where(x => x.Publicacao.Id == publicacao.Id)
                .And(x => x.Usuario.Id == usuario.Id)
                .List()
                .FirstOrDefault();
        }
    }
}