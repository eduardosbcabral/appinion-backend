using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.ApplicationService.CommandHandlers
{
    public class DiscordarNoticia : TransactionalCommandHandler<DiscordarNoticiaCommand>
    {
        private readonly IOpiniaoRepository _opiniaoRepository;

        public DiscordarNoticia(IUnitOfWork uow, IServiceContext serviceContext, ILogTransacaoRepository logTransacaoRepository, IOpiniaoRepository opiniaoRepository)
             : base(uow, serviceContext, logTransacaoRepository)
        {
            Uow = uow;
            _opiniaoRepository = opiniaoRepository;
        }

        public IUnitOfWork Uow { get; }

        protected override void OnPrepareTransactScope(DiscordarNoticiaCommand command)
        {
            Usuario usuario = new Usuario(command.Usuario.Id);
            Noticia noticia = new Noticia(command.Noticia.Id);

            Opiniao opiniao = new Opiniao(usuario, noticia);

            opiniao.Discordar();

            _opiniaoRepository.Save(opiniao);
        }
    }
}
