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
    public class SeguirUsuario : TransactionalCommandHandler<SeguirUsuarioCommand>
    {
        private readonly IServiceContext _serviceContext;
        private readonly ISeguidorUsuarioRepository _seguidorUsuarioRepository;

        public SeguirUsuario(
            IUnitOfWork uow,
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            ISeguidorUsuarioRepository seguidorUsuarioRepository
        ) : base(uow, serviceContext, logTransacaoRepository)
        {
            _serviceContext = serviceContext;

            _seguidorUsuarioRepository = seguidorUsuarioRepository;
        }

        protected override void OnPrepareTransactScope(SeguirUsuarioCommand command)
        {
            Usuario usuarioSeguidor = new Usuario(_serviceContext.UsuarioAtualId);
            Usuario usuarioSeguido = new Usuario(command.UsuarioSeguido.Id);

            var usuarioJaSeguido = _seguidorUsuarioRepository.UsuarioJaSeguido(usuarioSeguidor, usuarioSeguido);

            if(usuarioJaSeguido != null)
            {
                throw new UsuarioException("Usuario", "Você já está seguindo o usuário.");
            }

            SeguidorUsuario seguidorUsuario = new SeguidorUsuario(usuarioSeguidor, usuarioSeguido);

            _seguidorUsuarioRepository.Save(seguidorUsuario);
        }
    }
}
