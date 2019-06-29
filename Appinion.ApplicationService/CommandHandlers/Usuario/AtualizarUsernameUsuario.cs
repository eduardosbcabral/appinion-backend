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
    public class AtualizarUsernameUsuario : TransactionalCommandHandler<AtualizarUsernameUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IServiceContext _serviceContext;

        public AtualizarUsernameUsuario(
            IUnitOfWork uow,
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            IUsuarioRepository usuarioRepository)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _usuarioRepository = usuarioRepository;

            _serviceContext = serviceContext;
        }

        protected override void OnPrepareTransactScope(AtualizarUsernameUsuarioCommand command)
        {
            Usuario usuario = _usuarioRepository.Find(_serviceContext.UsuarioAtualId);

            usuario.AtualizarUsername(command.Username);

            _usuarioRepository.Update(usuario);
        }
    }
}
