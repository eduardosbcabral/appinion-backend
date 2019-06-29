using System;
using System.Collections.Generic;
using System.Text;
using Appinion.Domain.Enum;
using FluentValidation;

namespace Appinion.ApplicationService.Commands
{
    public class SeguirUsuarioCommand : ICommand
    {
        public TransacaoOpt Operacao => TransacaoOpt.SEGUIR_USUARIO;

        public CommandID UsuarioSeguido { get; set; }
    }

    public class SeguirUsuarioCommandCommandValidator : AbstractValidator<SeguirUsuarioCommand>
    {
        public SeguirUsuarioCommandCommandValidator()
        {
            RuleFor(x => x.UsuarioSeguido)
                .NotEmpty()
                .WithMessage("Parâmetro 'UsuarioSeguido' é obrigátório.");
        }
    }
}
