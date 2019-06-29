using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class InativarUsuarioCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao { get { return TransacaoOpt.INATIVAR_USUARIO; } }
    }

    public class InativarUsuarioCommandValidator : AbstractValidator<InativarUsuarioCommand>
    {
        public InativarUsuarioCommandValidator()
        {
        }
    }
}
