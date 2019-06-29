using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class AtualizarSenhaUsuarioCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao { get { return TransacaoOpt.ATUALIZAR_SENHA; } }

        public string Senha { get; set; }

        public AtualizarSenhaUsuarioCommand()
        {

        }
    }

    public class AtualizarSenhaUsuarioCommandValidator : AbstractValidator<AtualizarSenhaUsuarioCommand>
    {
        public AtualizarSenhaUsuarioCommandValidator()
        {
            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Informe a nova senha.");
        }
    }
}
