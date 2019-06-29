using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class RecuperarUsuarioComMaisUpvotesNaSemanaQueryModel
    {
        public int UsuarioId { get; set; }
        public int QuantidadeUpvotes { get; set; }
    }
}
