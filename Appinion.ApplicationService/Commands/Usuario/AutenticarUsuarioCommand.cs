using Appinion.Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class AutenticarUsuarioCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao { get { return TransacaoOpt.LOGIN; } }
        
        public string Username { get; set; }
        public string Senha { get; set; }

        public AutenticarUsuarioCommand()
        {

        }
    }

    public class AutenticarUsuarioCommandValidator : AbstractValidator<AutenticarUsuarioCommand>
    {
        public AutenticarUsuarioCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Informe o username.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Informe a senha.");
        }
    }
}
