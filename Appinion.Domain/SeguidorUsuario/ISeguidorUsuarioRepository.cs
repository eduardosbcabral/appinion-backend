using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Interface
{
    public interface ISeguidorUsuarioRepository : IRepository<SeguidorUsuario>
    {
        SeguidorUsuario UsuarioJaSeguido(Usuario usuarioSeguidor, Usuario usuarioSeguido);
    }
}
