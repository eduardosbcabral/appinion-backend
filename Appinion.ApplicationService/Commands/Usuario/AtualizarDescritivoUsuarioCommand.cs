using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class AtualizarDescritivoUsuarioCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao { get { return TransacaoOpt.ATUALIZAR_DESCRITIVO_USUARIO; } }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataNascimento { get; set; }

        public AtualizarDescritivoUsuarioCommand()
        {

        }
    }

    public class AtualizarDescritivoUsuarioCommandValidator : AbstractValidator<AtualizarDescritivoUsuarioCommand>
    {
        public AtualizarDescritivoUsuarioCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe o nome.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Informe a data de nascimento.");
        }
    }
}
