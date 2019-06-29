using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class InativarPublicacaoCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao => TransacaoOpt.INATIVAR_PUBLICACAO;

        public CommandID Publicacao { get; set; }
    }

    public class InativarPublicacaoCommandValidator : AbstractValidator<InativarPublicacaoCommand>
    {
        public InativarPublicacaoCommandValidator()
        {
            RuleFor(x => x.Publicacao)
                .NotEmpty().WithMessage("Parâmetro 'Publicacao' é necessário.");

            RuleFor(x => x.Publicacao.Id)
                .NotEmpty().WithMessage("Atributo 'Id' em 'Publicacao' é necessário.");
        }
    }
}
