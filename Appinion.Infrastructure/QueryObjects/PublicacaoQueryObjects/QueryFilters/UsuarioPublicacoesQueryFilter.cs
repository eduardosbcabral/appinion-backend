using Appinion.Infrastructure.QueryModels;
using Appinion.Infrastructure.QueryObjects.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryFilters
{
    public class UsuarioPublicacoesQueryFilter : IQueryFilter
    {
        public int Pagina { get; set; }
        public UsuarioQueryModel Usuario { get; set; }
    }
}
