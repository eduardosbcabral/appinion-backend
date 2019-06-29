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
    public class AtualizarEmailUsuarioCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao { get { return TransacaoOpt.ATUALIZAR_EMAIL_USUARIO; } }

        public string Email { get; set; }

        public AtualizarEmailUsuarioCommand()
        {

        }
    }

    public class AtualizarEmailUsuarioCommandValidator : AbstractValidator<AtualizarEmailUsuarioCommand>
    {
        public AtualizarEmailUsuarioCommandValidator(IUsuarioRepository usuarioRepository)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Informe o Email.")
                .EmailAddress().WithMessage("Informe um Email válido.")
                .Must(x => !usuarioRepository.EmailExiste(x)).WithMessage("Existe um usuário cadastrado com o Email informado.");
        }
    }
}
