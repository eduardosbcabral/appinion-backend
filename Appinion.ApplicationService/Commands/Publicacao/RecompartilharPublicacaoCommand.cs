using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class RecompartilharPublicacaoCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao => TransacaoOpt.RECOMPARTILHAR_PUBLICACAO;

        public CommandID PublicacaoRecompartilhada { get; set; }
        public string Conteudo { get; set; }
    }

    public class RecompartilharPublicacaoCommandValidator : AbstractValidator<RecompartilharPublicacaoCommand>
    {
        public RecompartilharPublicacaoCommandValidator()
        {
            RuleFor(x => x.PublicacaoRecompartilhada)
                .NotEmpty().WithMessage("Parâmetro 'PublicacaoRecompartilhada' é necessário.");

            RuleFor(x => x.PublicacaoRecompartilhada.Id)
                .NotEmpty().WithMessage("Atributo 'Id' em 'PublicacaoRecompartilhada' é necessário.");

            RuleFor(x => x.Conteudo)
                .NotEmpty().WithMessage("Informe o conteúdo.")
                .MaximumLength(256).WithMessage("O conteúdo deverá ter no máximo 256 caracteres");
        }
    }
}