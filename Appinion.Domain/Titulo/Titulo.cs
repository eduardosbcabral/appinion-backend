using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    public abstract class Titulo
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual int Quantidade { get; protected set; }
        public virtual DateTime Data { get; protected set; }
        public virtual bool Ativo { get; protected set; }

        protected Titulo() { }

        public Titulo(string nome, int quantidade)
        {
            Nome = nome;
            Quantidade = quantidade;
            Data = DateTime.Now;
            Ativo = true;
        }
    }
}
