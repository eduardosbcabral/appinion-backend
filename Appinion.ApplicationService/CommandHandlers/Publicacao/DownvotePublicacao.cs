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
    public class DownvotePublicacao : TransactionalCommandHandler<DownvotePublicacaoCommand>
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IPublicacaoDownvoteRepository _publicacaoDownvoteRepository;
        private readonly IPublicacaoUpvoteRepository _publicacaoUpvoteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        private IServiceContext _serviceContext;

        public DownvotePublicacao(
            IUnitOfWork uow,
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            IPublicacaoRepository publicacaoRepository,
            IPublicacaoDownvoteRepository publicacaoDownvoteRepository,
            IPublicacaoUpvoteRepository publicacaoUpvoteRepository,
            IUsuarioRepository usuarioRepository)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _publicacaoRepository = publicacaoRepository;
            _publicacaoDownvoteRepository = publicacaoDownvoteRepository;
            _publicacaoUpvoteRepository = publicacaoUpvoteRepository;
            _usuarioRepository = usuarioRepository;

            _serviceContext = serviceContext;
        }

        protected override void OnPrepareTransactScope(DownvotePublicacaoCommand command)
        {
            Usuario usuario = _usuarioRepository.Find(_serviceContext.UsuarioAtualId);

            Publicacao publicacao = _publicacaoRepository.Find(command.Publicacao.Id);

            PublicacaoDownvote downvoteExistente = _publicacaoDownvoteRepository.PodeDownvote(publicacao, usuario);

            if (downvoteExistente != null)
            {
                publicacao.Upvote();
                _publicacaoDownvoteRepository.Delete(downvoteExistente);
                return;
            }

            // Excluir upvote caso exista no momento do downvote
            PublicacaoUpvote upvoteExistente = _publicacaoUpvoteRepository.PodeUpvote(publicacao, usuario);

            if (upvoteExistente != null)
            {
                publicacao.Downvote();
                _publicacaoUpvoteRepository.Delete(upvoteExistente);
            }

            publicacao.Downvote();

            PublicacaoDownvote publicacaoDownvote = new PublicacaoDownvote(publicacao, usuario);

            _publicacaoDownvoteRepository.Save(publicacaoDownvote);
        }
    }
}
