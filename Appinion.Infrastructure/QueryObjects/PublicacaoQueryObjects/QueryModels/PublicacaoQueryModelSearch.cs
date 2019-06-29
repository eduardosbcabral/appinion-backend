using Appinion.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class PublicacaoQueryModelSearch
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public int QuantidadeVotos { get; set; }
        public DateTime Data { get; set; }
        public IList<ArquivoQueryModel> Imagens { get; set; }
        public UsuarioQueryModelWithoutList Usuario { get; set; }
    }
}
