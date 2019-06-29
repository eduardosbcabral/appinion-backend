using Appinion.Infrastructure.QueryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class SeguidorUsuarioQueryModel
    {
        public UsuarioQueryModelWithoutList UsuarioSeguidor { get; set; }
        public UsuarioQueryModelWithoutList UsuarioSeguido { get; set; }
        public virtual DateTime Data { get; protected set; }
    }
}
