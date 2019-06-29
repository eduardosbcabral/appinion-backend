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
    public class PararSeguirUsuario : TransactionalCommandHandler<PararSeguirUsuarioCommand>
    {
        private readonly IServiceContext _serviceContext;
        private readonly ISeguidorUsuarioRepository _seguidorUsuarioRepository;

        public PararSeguirUsuario(
            IUnitOfWork uow,
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            ISeguidorUsuarioRepository seguidorUsuarioRepository
        ) : base(uow, serviceContext, logTransacaoRepository)
        {
            _serviceContext = serviceContext;

            _seguidorUsuarioRepository = seguidorUsuarioRepository;
        }

        protected override void OnPrepareTransactScope(PararSeguirUsuarioCommand command)
        {
            Usuario usuarioSeguidor = new Usuario(_serviceContext.UsuarioAtualId);
            Usuario usuarioSeguido = new Usuario(command.Usuario.Id);

            var usuarioJaSeguido = _seguidorUsuarioRepository.UsuarioJaSeguido(usuarioSeguidor, usuarioSeguido);

            if (usuarioJaSeguido == null)
            {
                throw new UsuarioException("Usuario", "Você não está seguindo o usuário.");
            }

            _seguidorUsuarioRepository.Delete(usuarioJaSeguido);
        }
    }
}
