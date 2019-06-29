using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.Domain.Entity;
using Appinion.Domain.Exceptions;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.CommandHandlers
{
    public class UpvotePublicacao : TransactionalCommandHandler<UpvotePublicacaoCommand>
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IPublicacaoUpvoteRepository _publicacaoUpvoteRepository;
        private readonly IPublicacaoDownvoteRepository _publicacaoDownvoteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        private IServiceContext _serviceContext;

        public UpvotePublicacao(
            IUnitOfWork uow,
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            IPublicacaoRepository publicacaoRepository,
            IPublicacaoUpvoteRepository publicacaoUpvoteRepository,
            IPublicacaoDownvoteRepository publicacaoDownvoteRepository,
            IUsuarioRepository usuarioRepository)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _publicacaoRepository = publicacaoRepository;
            _publicacaoUpvoteRepository = publicacaoUpvoteRepository;
            _publicacaoDownvoteRepository = publicacaoDownvoteRepository;
            _usuarioRepository = usuarioRepository;

            _serviceContext = serviceContext;
        }

        protected override void OnPrepareTransactScope(UpvotePublicacaoCommand command)
        {
            Usuario usuario = _usuarioRepository.Find(_serviceContext.UsuarioAtualId);
            Publicacao publicacao = _publicacaoRepository.Find(command.Publicacao.Id);

            PublicacaoUpvote upvoteExistente = _publicacaoUpvoteRepository.PodeUpvote(publicacao, usuario);

            if(upvoteExistente != null)
            {
                publicacao.Downvote();
                _publicacaoUpvoteRepository.Delete(upvoteExistente);
                return;
            }

            // Excluir downvote caso exista no momento do upvote
            PublicacaoDownvote downvoteExistente = _publicacaoDownvoteRepository.PodeDownvote(publicacao, usuario);
            if (downvoteExistente != null)
            {
                publicacao.Upvote();
                _publicacaoDownvoteRepository.Delete(downvoteExistente);
            }

            publicacao.Upvote();

            PublicacaoUpvote publicacaoUpvote = new PublicacaoUpvote(publicacao, usuario);

            _publicacaoUpvoteRepository.Save(publicacaoUpvote);
        }
    }
}
