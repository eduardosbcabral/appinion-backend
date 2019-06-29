using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class UpvotePublicacaoCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao => TransacaoOpt.UPVOTE_PUBLICACAO;

        public CommandID Publicacao { get; set; }
    }

    public class UpvotePublicacaoCommandValidator : AbstractValidator<UpvotePublicacaoCommand>
    {
        public UpvotePublicacaoCommandValidator()
        {
            RuleFor(x => x.Publicacao)
                .NotEmpty().WithMessage("Parâmetro 'Publicacao' é necessário.");

            RuleFor(x => x.Publicacao.Id)
                .NotEmpty().WithMessage("Atributo 'Id' em 'Publicacao' é necessário.");
        }
    }
}
