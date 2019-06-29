using Appinion.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Interface
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        bool UsuarioExiste(string username);
        bool EmailExiste(string email);
        string RecuperarSenhaCriptografada(string username);
        Usuario RecuperarUsuarioPeloUsername(string username);
    }
}
