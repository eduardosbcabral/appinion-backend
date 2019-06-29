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
    public class ConcordarNoticia : TransactionalCommandHandler<ConcordarNoticiaCommand>
    {
        private readonly IOpiniaoRepository _opiniaoRepository;

        public ConcordarNoticia(IUnitOfWork uow, IServiceContext serviceContext, ILogTransacaoRepository logTransacaoRepository, IOpiniaoRepository opiniaoRepository)
             : base(uow, serviceContext, logTransacaoRepository)
        {
            _opiniaoRepository = opiniaoRepository;
        }

        protected override void OnPrepareTransactScope(ConcordarNoticiaCommand command)
        {
            Usuario usuario = new Usuario(command.Usuario.Id);
            Noticia noticia = new Noticia(command.Noticia.Id);

            Opiniao opiniao = new Opiniao(usuario, noticia);

            opiniao.Concordar();

            _opiniaoRepository.Save(opiniao);
        }
    }
}
