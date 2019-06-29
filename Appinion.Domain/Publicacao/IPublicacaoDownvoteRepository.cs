using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Interface
{
    public interface IPublicacaoDownvoteRepository : IRepository<PublicacaoDownvote>
    {
        PublicacaoDownvote PodeDownvote(Publicacao publicacao, Usuario usuario);
    }
}
