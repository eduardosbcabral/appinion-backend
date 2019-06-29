using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    public class TituloNoticia : Titulo
    {
        public virtual Noticia Noticia { get; protected set; }

        protected TituloNoticia() { }

        public TituloNoticia(string nome, int quantidade, Noticia noticia)
            : base(nome, quantidade)
        {
            Noticia = noticia;
        }
    }
}
