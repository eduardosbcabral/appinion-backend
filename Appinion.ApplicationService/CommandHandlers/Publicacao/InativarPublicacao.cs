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
    public class InativarPublicacao : TransactionalCommandHandler<InativarPublicacaoCommand>
    {
        private readonly IPublicacaoRepository _publicacaoRepository;

        public InativarPublicacao(IUnitOfWork uow, IServiceContext serviceContext, ILogTransacaoRepository logTransacaoRepository, IPublicacaoRepository publicacaoRepository)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _publicacaoRepository = publicacaoRepository;
        }

        protected override void OnPrepareTransactScope(InativarPublicacaoCommand command)
        {
            Publicacao publicacao = _publicacaoRepository.Find(command.Publicacao.Id);

            publicacao.Inativar();

            _publicacaoRepository.Update(publicacao);
        }
    }
}
