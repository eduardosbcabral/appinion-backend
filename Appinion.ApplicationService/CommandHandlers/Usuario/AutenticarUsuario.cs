using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.Domain.Config;
using Appinion.Domain.Entity;
using Appinion.Domain.Exceptions;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Appinion.ApplicationService.CommandHandlers
{
    public class AutenticarUsuario : TransactionalCommandHandler<AutenticarUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly AppSettingsModel _appSettings;

        public AutenticarUsuario(IUnitOfWork uow, IServiceContext serviceContext, ILogTransacaoRepository logTransacaoRepository, IUsuarioRepository usuarioRepository, IOptions<AppSettingsModel> appSettings)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _usuarioRepository = usuarioRepository;

            _appSettings = appSettings.Value;
        }

        protected override void OnPrepareTransactScope(AutenticarUsuarioCommand command)
        {
            var usuarioExiste = _usuarioRepository.UsuarioExiste(command.Username);

            if (!usuarioExiste)
            {
                throw new UsuarioException("Login", "Username ou senha incorretos.");
            }

            var senhaValida = BCrypt.Net.BCrypt.Verify(command.Senha, _usuarioRepository.RecuperarSenhaCriptografada(command.Username));

            if (!senhaValida)
            {
                throw new UsuarioException("Login", "Username ou senha incorretos.");
            }

            var u = _usuarioRepository.RecuperarUsuarioPeloUsername(command.Username);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, u.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            this.CreateResult(new AutenticarUsuarioResultCommand()
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Tipo.ToString(),
                Token = tokenString
            });

        }
    }
}