using Appinion.Domain.Enum;
using Appinion.Domain.Interface;
using FluentValidation;
using FluentValidation.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.Commands
{
    public class CadastrarUsuarioCommand : ICommand
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransacaoOpt Operacao { get { return TransacaoOpt.CADASTRAR_USUARIO; } }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public CadastrarUsuarioCommand()
        {

        }
    }

    public class CadastrarUsuarioCommandValidator : AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidator(IUsuarioRepository usuarioRepository)
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Informe o username.")
                .Must(x => !usuarioRepository.UsuarioExiste(x)).WithMessage("Existe um usuário cadastrado com o Username informado.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Informe o Email")
                .EmailAddress().WithMessage("Informe um Email válido.")
                .Must(x => !usuarioRepository.EmailExiste(x)).WithMessage("Existe um usuário cadastrado com o Email informado.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Informe a Senha.")
                .Length(6, 128)
                .WithMessage("A senha deverá ter no mínimo 6 caracteres.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe o Nome.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Informe a Data de Nascimento.");
        }
    }
}
