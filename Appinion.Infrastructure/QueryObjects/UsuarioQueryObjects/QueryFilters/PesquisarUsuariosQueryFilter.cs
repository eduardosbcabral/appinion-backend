using Appinion.Infrastructure.QueryObjects.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryFilters
{
    public class PesquisarUsuariosQueryFilter : IQueryFilter
    {
        public string Descritivo { get; set; }
        public int Pagina { get; set; }
    }
}
