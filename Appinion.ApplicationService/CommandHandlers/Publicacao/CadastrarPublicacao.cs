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
using System.Text;

namespace Appinion.ApplicationService.CommandHandlers
{
    public class CadastrarPublicacao : TransactionalCommandHandler<CadastrarPublicacaoCommand>
    {
        private readonly IPublicacaoRepository _publicacaoRepository;

        private readonly IServiceContext _serviceContext;
        private IOptions<UploadSettingsModel> _uploadSettings;

        public CadastrarPublicacao(IUnitOfWork uow, IServiceContext serviceContext, ILogTransacaoRepository logTransacaoRepository, IPublicacaoRepository publicacaoRepository, IOptions<UploadSettingsModel> uploadSettings)
            : base(uow, serviceContext, logTransacaoRepository)
        {
            _publicacaoRepository = publicacaoRepository;

            _serviceContext = serviceContext;
            _uploadSettings = uploadSettings;
        }

        protected override void OnPrepareTransactScope(CadastrarPublicacaoCommand command)
        {
            Usuario usuario = new Usuario(_serviceContext.UsuarioAtualId);
            Publicacao publicacao = new Publicacao(command.Conteudo, usuario);

            foreach(var imagem in command.Imagens)
            {
                Arquivo arquivo = new UploadService(_uploadSettings)
                .Arquivo(imagem)
                .Tipo(TipoArquivo.PUBLICACAO)
                .Usuario(_serviceContext.UsuarioAtualId)
                .SalvarArquivo();

                publicacao.AdicionarImagem(arquivo);
            }

            _publicacaoRepository.Save(publicacao);
        }
    }
}
