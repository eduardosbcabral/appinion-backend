using Appinion.Infrastructure.QueryObjects.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryFilters
{
    public class ListarNoticiasParaOpinarQueryFilter : IQueryFilter
    {
        public int Pagina { get; set; }
    }
}
