using Appinion.Domain.Enum;
using Appinion.Domain.Interface;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class AtualizarUsernameUsuarioCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao { get { return TransacaoOpt.ATUALIZAR_USERNAME_USUARIO; } }

        public string Username { get; set; }

        public AtualizarUsernameUsuarioCommand()
        {

        }
    }

    public class AtualizarUsernameUsuarioCommandCommandValidator : AbstractValidator<AtualizarUsernameUsuarioCommand>
    {
        public AtualizarUsernameUsuarioCommandCommandValidator(IUsuarioRepository usuarioRepository)
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Informe o username.")
                .Must(x => !usuarioRepository.UsuarioExiste(x)).WithMessage("Existe um usuário cadastrado com o Username informado.");

        }
    }
}