using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.CommandHandlers
{
    public class RecompartilharPublicacao : TransactionalCommandHandler<RecompartilharPublicacaoCommand>
    {
        private readonly IPublicacaoRepository _publicacaoRepository;

        private readonly IServiceContext _serviceContext;

        public RecompartilharPublicacao(IUnitOfWork uow, IServiceContext serviceContext, ILogTransacaoRepository logTransacaoRepository, IPublicacaoRepository publicacaoRepository)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _publicacaoRepository = publicacaoRepository;

            _serviceContext = serviceContext;
        }

        protected override void OnPrepareTransactScope(RecompartilharPublicacaoCommand command)
        {
            Usuario usuario = new Usuario(_serviceContext.UsuarioAtualId);
            Publicacao publicacao = new Publicacao(command.Conteudo, usuario);

            Publicacao publicacaoRecompartilhada = new Publicacao(command.PublicacaoRecompartilhada.Id);

            publicacao.Recompartilhar(publicacaoRecompartilhada);
            
            _publicacaoRepository.Save(publicacao);
        }
    }
}