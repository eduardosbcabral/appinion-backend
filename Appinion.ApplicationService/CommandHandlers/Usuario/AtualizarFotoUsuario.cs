using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.ApplicationService.Services;
using Appinion.Domain.Config;
using Appinion.Domain.Entity;
using Appinion.Domain.Enum;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Appinion.ApplicationService.CommandHandlers
{
    public class AtualizarFotoUsuario : TransactionalCommandHandler<AtualizarFotoUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IServiceContext _serviceContext;
        private IOptions<UploadSettingsModel> _uploadSettings;

        public AtualizarFotoUsuario(
            IUnitOfWork uow,
            IServiceContext serviceContext,
            ILogTransacaoRepository logTransacaoRepository,
            IUsuarioRepository usuarioRepository,
            IOptions<UploadSettingsModel> uploadSettings)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _usuarioRepository = usuarioRepository;

            _serviceContext = serviceContext;
            _uploadSettings = uploadSettings;
        }

        protected override void OnPrepareTransactScope(AtualizarFotoUsuarioCommand command)
        {
            Usuario usuario = _usuarioRepository.Find(_serviceContext.UsuarioAtualId);

            Arquivo foto = new UploadService(_uploadSettings)
                .Arquivo(command.Foto)
                .Tipo(TipoArquivo.FOTO)
                .Usuario(_serviceContext.UsuarioAtualId)
                .SalvarArquivo();
            
            usuario.AtualizarFoto(foto);

            _usuarioRepository.Update(usuario);
        }
    }
}
