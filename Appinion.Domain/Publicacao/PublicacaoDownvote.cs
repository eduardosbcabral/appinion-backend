using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    public class PublicacaoDownvote
    {
        public virtual Publicacao Publicacao { get; protected set; }
        public virtual Usuario Usuario { get; protected set; }
        public virtual DateTime Data { get; protected set; }

        public PublicacaoDownvote()
        {

        }

        public PublicacaoDownvote(Publicacao publicacao, Usuario usuario)
        {
            Publicacao = publicacao;
            Usuario = usuario;
            Data = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PublicacaoDownvote;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Publicacao == other.Publicacao &&
                this.Usuario == other.Usuario;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ Publicacao.GetHashCode();
                hash = (hash * 31) ^ Usuario.GetHashCode();

                return hash;
            }
        }
    }
}
