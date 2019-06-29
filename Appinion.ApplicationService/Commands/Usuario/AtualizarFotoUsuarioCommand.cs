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
    public class AtualizarFotoUsuarioCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao { get { return TransacaoOpt.ATUALIZAR_FOTO_USUARIO; } }

        public IFormFile Foto { get; set; }

        public AtualizarFotoUsuarioCommand()
        {

        }
    }

    public class AtualizarFotoUsuarioCommandValidator : AbstractValidator<AtualizarFotoUsuarioCommand>
    {
        public AtualizarFotoUsuarioCommandValidator()
        {
            RuleFor(x => x.Foto)
                .NotEmpty().WithMessage("Insira a imagem a ser atualizada.");
        }
    }
}
