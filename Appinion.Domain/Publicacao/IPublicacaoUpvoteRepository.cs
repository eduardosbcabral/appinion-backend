using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Interface
{
    public interface IPublicacaoUpvoteRepository : IRepository<PublicacaoUpvote>
    {
        PublicacaoUpvote PodeUpvote(Publicacao publicacao, Usuario usuario);
    }
}
