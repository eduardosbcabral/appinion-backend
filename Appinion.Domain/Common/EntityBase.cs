using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Entidade base para as Classes de Domínio que usam o ID como chave primária.
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Chave primária da entidade.
        /// </summary>
        public virtual int Id { get; protected set; }

        protected EntityBase()
        {

        }
    }
}
