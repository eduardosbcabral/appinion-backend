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
    public class AtualizarSenhaUsuario : TransactionalCommandHandler<AtualizarSenhaUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IServiceContext _serviceContext;

        public AtualizarSenhaUsuario(
            IUnitOfWork uow,
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            IUsuarioRepository usuarioRepository)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _usuarioRepository = usuarioRepository;

            _serviceContext = serviceContext;
        }

        protected override void OnPrepareTransactScope(AtualizarSenhaUsuarioCommand command)
        {
            Usuario usuario = _usuarioRepository.Find(_serviceContext.UsuarioAtualId);

            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(command.Senha);

            usuario.AtualizarSenha(senhaCriptografada);

            _usuarioRepository.Update(usuario);
        }
    }
}