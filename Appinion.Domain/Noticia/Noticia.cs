using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Uma das principais entidades do sistema, representando uma Notícia.
    /// </summary>
    public class Noticia : EntityBase
    {
        /// <summary>
        /// Representa o título da Notícia.
        /// </summary>
        public virtual string Titulo { get; protected set; }
        /// <summary>
        /// Representa a URL da Notícia.
        /// </summary>
        public virtual string Url { get; protected set; }
        /// <summary>
        /// Repreenta a URL da imagem da Notícia.
        /// </summary>
        public virtual string UrlImagem { get; protected set; }
        /// <summary>
        /// Representa uma breve descrição do conteúdo geral da Notícia.
        /// </summary>
        public virtual string Conteudo { get; protected set; }
        /// <summary>
        /// Representa a data da Notícia.
        /// </summary>
        public virtual DateTime Data { get; protected set; }
        /// <summary>
        /// Representa o estado booleano da Notícia
        /// </summary>
        public virtual bool Ativo { get; protected set; }

        protected Noticia()
        {

        }

        public Noticia(int id)
        {
            Id = id;
        }
    }
}
