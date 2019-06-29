using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Representa a Opinião do Usuário em relação à Notícia.
    /// </summary>
    public class Opiniao : EntityBase
    {
        /// <summary>
        /// Representa o Tipo da Opinião.
        /// </summary>
        public virtual TipoOpiniao Tipo { get; protected set; }
        /// <summary>
        /// Representa a Notícia que o Usuário opinou.
        /// </summary>
        public virtual Noticia Noticia { get; protected set; }
        /// <summary>
        /// Representa o Usuário que opinou.
        /// </summary>
        public virtual Usuario Usuario { get; protected set; }

        public virtual DateTime Data { get; protected set; }

        protected Opiniao()
        {

        }

        public Opiniao(Usuario usuario, Noticia noticia)
        {
            Usuario = usuario;
            Noticia = noticia;
            Data = DateTime.Now;
        }

        public virtual void Concordar()
        {
            Tipo = TipoOpiniao.Concordo;
        }

        public virtual void Discordar()
        {
            Tipo = TipoOpiniao.Discordo;
        }
    }
}
