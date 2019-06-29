using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Helper;
using Appinion.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.CommandHandlers
{
    public abstract class TransactionalCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public IResultCommand ResultCommand { get; private set; }

        private bool _logTransacao = true;

        private readonly IUnitOfWork _uow;
        private readonly IServiceContext _serviceContext;
        private readonly ILogTransacaoRepository _logTransacaoRepository;

        public TransactionalCommandHandler(IUnitOfWork uow, IServiceContext serviceContext, ILogTransacaoRepository logTransacaoRepository)
        {
            _uow = uow;
            _serviceContext = serviceContext;
            _logTransacaoRepository = logTransacaoRepository;
        }

        protected virtual void OnPrepareTransactScope(TCommand command)
        {

        }

        public void Execute(TCommand command)
        {
            _uow.BeginTransaction();

            OnPrepareTransactScope(command);

            if (_logTransacao)
            {
                OnLogTransacao(command);
            }

            _uow.Execute();
        }

        private void OnLogTransacao(TCommand command)
        {
            LogTransacao lt = new LogTransacao(command.Operacao, _serviceContext.UsuarioAtual, FormatHelper.Serializer(command), _serviceContext.ParamsToJson());

            _logTransacaoRepository.Save(lt);
        }

        public void CreateResult(IResultCommand resultCommand)
        {
            ResultCommand = resultCommand;
        }
    }
}
