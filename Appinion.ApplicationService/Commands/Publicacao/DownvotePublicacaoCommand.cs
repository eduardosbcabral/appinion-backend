using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class DownvotePublicacaoCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao => TransacaoOpt.DOWNVOTE_PUBLICACAO;

        public CommandID Publicacao { get; set; }
    }

    public class DownvotePublicacaoCommandValidator : AbstractValidator<DownvotePublicacaoCommand>
    {
        public DownvotePublicacaoCommandValidator()
        {
            RuleFor(x => x.Publicacao)
                .NotEmpty().WithMessage("Parâmetro 'Publicacao' é necessário.");

            RuleFor(x => x.Publicacao.Id)
                .NotEmpty().WithMessage("Atributo 'Id' em 'Publicacao' é necessário.");
        }
    }
}
