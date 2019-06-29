using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class ConcordarNoticiaCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao => TransacaoOpt.CONCORDAR_NOTICIA;

        public CommandID Usuario { get; set; }
        public CommandID Noticia { get; set; }

        public ConcordarNoticiaCommand()
        {

        }
    }

    public class ConcordarNoticiaCommandValidator : AbstractValidator<ConcordarNoticiaCommand>
    {
        public ConcordarNoticiaCommandValidator()
        {
            RuleFor(x => x.Usuario)
                .NotEmpty()
                .WithMessage("Parâmetro 'Usuario' é necessário.");

            RuleFor(x => x.Usuario.Id)
                .NotEmpty()
                .WithMessage("Atributo 'Id' em 'Usuario' é necessário.");

            RuleFor(x => x.Noticia)
                .NotEmpty()
                .WithMessage("Parâmetro 'Noticia' é necessário.");

            RuleFor(x => x.Noticia.Id)
                .NotEmpty()
                .WithMessage("Atributo 'Id' em 'Usuario' é necessário.");
        }
    }
}
