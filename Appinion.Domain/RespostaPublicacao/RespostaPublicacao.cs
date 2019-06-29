using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Entidade auxiliar que tem a função de facilitar o agrupamento de Publicações e Usuários destino.
    /// </summary>
    public class RespostaPublicacao : EntityBase
    {
        /// <summary>
        /// Representa o Usuário destino da publicação.
        /// </summary>
        public virtual Usuario UsuarioDestino { get; protected set; }
        /// <summary>
        /// Representa a Publicação.
        /// </summary>
        public virtual Publicacao Publicacao { get; protected set; }

        protected RespostaPublicacao()
        {

        }
    }
}
