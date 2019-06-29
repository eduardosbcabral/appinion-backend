using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Representa a ação Seguir, quando um Usuário segue outro na rede social.
    /// </summary>
    public class SeguidorUsuario
    {
        /// <summary>
        /// Representa o Usuário origem, ou seja, que efetuou a ação de Seguir.
        /// </summary>
        public virtual Usuario UsuarioSeguidor { get; protected set; }
        /// <summary>
        /// Representa o Usuário destino, ou seja, o que foi seguido.
        /// </summary>
        public virtual Usuario UsuarioSeguido { get; protected set; }
        /// <summary>
        /// Representa a Data no momento da ação.
        /// </summary>
        public virtual DateTime Data { get; protected set; }

        protected SeguidorUsuario()
        {

        }

        public SeguidorUsuario(Usuario usuarioSeguidor, Usuario usuarioSeguido)
        {
            UsuarioSeguidor = usuarioSeguidor;
            UsuarioSeguido = usuarioSeguido;
            Data = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            var other = obj as SeguidorUsuario;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.UsuarioSeguidor == other.UsuarioSeguidor &&
                this.UsuarioSeguido == other.UsuarioSeguido;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ UsuarioSeguidor.GetHashCode();
                hash = (hash * 31) ^ UsuarioSeguido.GetHashCode();

                return hash;
            }
        }
    }
}
