using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    public class TituloUsuario : Titulo
    {
        public virtual Usuario Usuario { get; protected set; }

        protected TituloUsuario() { }

        public TituloUsuario(string nome, int quantidade, Usuario usuario)
            : base(nome, quantidade)
        {
            Usuario = usuario;
        }
    }
}
