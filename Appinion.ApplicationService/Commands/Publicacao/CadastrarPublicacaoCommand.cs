using Appinion.Domain.Enum;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class CadastrarPublicacaoCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao => TransacaoOpt.CADASTRAR_PUBLICACAO;

        public string Conteudo { get; set; }
        public IList<IFormFile> Imagens { get; set; }

        public CadastrarPublicacaoCommand()
        {
            Imagens = new List<IFormFile>();
        }
    }

    public class CadastrarPublicacaoCommandValidator : AbstractValidator<CadastrarPublicacaoCommand>
    {
        public CadastrarPublicacaoCommandValidator()
        {
            RuleFor(x => x.Conteudo)
                .NotEmpty().WithMessage("Informe o conteúdo.")
                .MaximumLength(256).WithMessage("O conteúdo deverá ter no máximo 256 caracteres");

            RuleFor(x => x.Imagens.Count)
                .LessThanOrEqualTo(4).WithMessage("Quantidade máxima de imagens excedida. Máximo: 4");
        }
    }
}
