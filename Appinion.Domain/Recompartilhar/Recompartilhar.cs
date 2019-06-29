using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Representa a função de recompartilhamento de publicação.
    /// </summary>
    public class Recompartilhar : EntityBase
    {
        /// <summary>
        /// Representa o momento atual do recompartilhamento.
        /// </summary>
        public virtual DateTime Data { get; protected set; }
        /// <summary>
        /// Representa o usuário que efetuou o recompartilhamento.
        /// </summary>
        public virtual Usuario Usuario { get; protected set; }
        /// <summary>
        /// Representa a publicação alvo do recompartilhamento.
        /// </summary>
        public virtual Publicacao Publicacao { get; protected set; }

        protected Recompartilhar()
        {

        }
    }
}
