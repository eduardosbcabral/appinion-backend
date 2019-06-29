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
    public class AtualizarDescritivoUsuario : TransactionalCommandHandler<AtualizarDescritivoUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IServiceContext _serviceContext;

        public AtualizarDescritivoUsuario(
            IUnitOfWork uow,
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            IUsuarioRepository usuarioRepository)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _usuarioRepository = usuarioRepository;

            _serviceContext = serviceContext;
        }

        protected override void OnPrepareTransactScope(AtualizarDescritivoUsuarioCommand command)
        {
            Usuario usuario = _usuarioRepository.Find(_serviceContext.UsuarioAtualId);

            usuario.AtualizarDescritivo(command.Nome, command.Descricao, command.DataNascimento);

            _usuarioRepository.Update(usuario);
        }
    }
}
