using Appinion.Infrastructure.QueryObjects.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryFilters
{
    public class PesquisarPublicacoesQueryFilter : IQueryFilter
    {
        public string Descritivo { get; set; }
        public int Pagina { get; set; }
    }
}
