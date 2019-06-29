using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class RecuperarUsuarioComMaisDownvotesNaSemanaQueryModel
    {
        public int UsuarioId { get; set; }
        public int QuantidadeDownvotes { get; set; }
    }
}
