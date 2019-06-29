using Appinion.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Representa qualquer tipo Arquivo inserido pelo Usuário na aplicação.
    /// </summary>
    public class Arquivo : EntityBase
    {
        /// <summary>
        /// Representa o Nome do Arquivo.
        /// </summary>
        public virtual string Nome { get; protected set; }
        /// <summary>
        /// Representa a Data em que o Arquvio foi inserido.
        /// </summary>
        public virtual DateTime Data { get; protected set; }
        /// <summary>
        /// Representa o Tamanho total do Arquivo em bytes.
        /// </summary>
        public virtual double Tamanho { get; protected set; }
        /// <summary>
        /// Representa o estado booleano do Arquivo
        /// </summary>
        public virtual bool Ativo { get; protected set; }
        /// <summary>
        /// Representa qual o tipo do Arquivo.
        /// </summary>
        public virtual TipoArquivo Tipo { get; protected set; }
        /// <summary>
        /// Representa o Usuário que inseriu o Arquivo.
        /// </summary>
        public virtual Usuario Usuario { get; protected set; }

        /// <summary>
        /// Representa uma regra do negócio que é o limite do tamanho do arquivo.
        /// </summary>
        public static double LimiteUpload
        {
            get { return 1000000; }
        }

        protected Arquivo()
        {

        }

        public Arquivo(string nome, double tamanho, Usuario usuario, TipoArquivo tipo)
        {
            Nome = nome;
            Tamanho = tamanho;
            Ativo = true;
            Data = DateTime.Now;
            Usuario = usuario;
            Tipo = tipo;
        }
    }
}
