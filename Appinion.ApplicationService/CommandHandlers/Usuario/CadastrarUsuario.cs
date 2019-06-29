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
    public class CadastrarUsuario : TransactionalCommandHandler<CadastrarUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarUsuario(
            IUnitOfWork uow, 
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            IUsuarioRepository usuarioRepository)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        protected override void OnPrepareTransactScope(CadastrarUsuarioCommand command)
        {
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(command.Senha);

            Usuario usuario = new Usuario(command.Username, command.Email, senhaCriptografada, command.Nome, command.DataNascimento);

            _usuarioRepository.Save(usuario);
        }
    }
}
