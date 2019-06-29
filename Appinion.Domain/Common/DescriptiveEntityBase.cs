using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Entidade base para entidades com funções descritivas.
    /// </summary>
    public class DescriptiveEntityBase : EntityBase
    {
        /// <summary>
        /// Representa a descrição da Classe de Domínio.
        /// </summary>
        public virtual string Descricao { get; protected set; }

        protected DescriptiveEntityBase()
        {

        }
    }
}
