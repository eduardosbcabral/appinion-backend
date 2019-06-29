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
    public class ComentarPublicacaoCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao => TransacaoOpt.COMENTAR_PUBLICACAO;

        public CommandID PublicacaoComentada { get; set; }
        public string Conteudo { get; set; }
        public IList<IFormFile> Imagens { get; set; }

        public ComentarPublicacaoCommand()
        {
            Imagens = new List<IFormFile>();
        }
    }

    public class ComentarPublicacaoCommandValidator : AbstractValidator<ComentarPublicacaoCommand>
    {
        public ComentarPublicacaoCommandValidator()
        {
            RuleFor(x => x.PublicacaoComentada)
                .NotEmpty().WithMessage("Parâmetro 'PublicacaoComentada' é necessário.");

            RuleFor(x => x.PublicacaoComentada.Id)
                .NotEmpty().WithMessage("Atributo 'Id' em 'PublicacaoComentada' é necessário.");

            RuleFor(x => x.Conteudo)
                .NotEmpty().WithMessage("Informe o conteúdo.")
                .MaximumLength(256).WithMessage("O conteúdo deverá ter no máximo 256 caracteres");

            RuleFor(x => x.Imagens.Count)
                .LessThan(4).WithMessage("Quantidade máxima de imagens excedida. Máximo: 4");
        }
    }
}
