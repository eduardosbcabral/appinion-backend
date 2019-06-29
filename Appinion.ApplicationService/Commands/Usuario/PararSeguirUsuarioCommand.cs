using Appinion.Domain.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class PararSeguirUsuarioCommand : ICommand
    {
        public TransacaoOpt Operacao => TransacaoOpt.PARAR_SEGUIR_USUARIO;

        public CommandID Usuario { get; set; }
    }

    public class PararSeguirUsuarioCommandValidator : AbstractValidator<PararSeguirUsuarioCommand>
    {
        public PararSeguirUsuarioCommandValidator()
        {
            RuleFor(x => x.Usuario)
                .NotEmpty()
                .WithMessage("Parâmetro 'Usuario' é obrigátório.");
        }
    }
}
