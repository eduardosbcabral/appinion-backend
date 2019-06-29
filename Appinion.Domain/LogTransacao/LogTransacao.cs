using Appinion.Domain.Entity;
using Appinion.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Classe que representa o Log das operações de escrita da aplicação para possíveis auditorias.
    /// </summary>
    public class LogTransacao : EntityBase
    {
        /// <summary>
        /// Representa o nome da operação de escrita da aplicação em que o usuário está manipulando.
        /// </summary>
        public virtual TransacaoOpt Operacao { get; protected set; }
        /// <summary>
        /// Representa o Id do Usuário que está fazendo a operação de escrita.
        /// </summary>
        public virtual string UsuarioId { get; protected set; }
        /// <summary>
        /// Representa a Data atual do momento da operação de escrita.
        /// </summary>
        public virtual DateTime Data { get; protected set; }
        /// <summary>
        /// Representa o objeto JSON da operação de escrita.
        /// </summary>
        public virtual string Objeto { get; protected set; }
        /// <summary>
        /// Representa a Origem da operação, ou seja, informações sobre o navegador do usuário, versão, etc...
        /// </summary>
        public virtual string Origem { get; protected set; }

        protected LogTransacao()
        {

        }

        public LogTransacao(TransacaoOpt operacao, string usuarioId, string objeto, string origem)
        {
            Operacao = operacao;
            UsuarioId = usuarioId;
            Objeto = objeto;
            Origem = origem;
            Data = DateTime.Now;
        }
    }
}
