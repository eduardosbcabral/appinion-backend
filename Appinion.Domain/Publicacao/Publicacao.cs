using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Representa a publicação do usuário na rede social.
    /// </summary>
    public class Publicacao : EntityBase
    {
        /// <summary>
        /// Representa o conteúdo da Publicação com a quantidade de caracteres máximo igual a 256.
        /// </summary>
        public virtual string Conteudo { get; protected set; }
        /// <summary>
        /// Representa a quantidade atual de votos da Publicação. 
        /// O valor poderá diminuir ou aumentar.
        /// </summary>
        public virtual int QuantidadeVotos { get; protected set; }
        /// <summary>
        /// Representa a data do momento da Publicação.
        /// </summary>
        public virtual DateTime Data { get; protected set; }
        /// <summary>
        /// Representa o Usuário que fez a Publicação.
        /// </summary>
        public virtual Usuario Usuario { get; protected set; }
        /// <summary>
        /// Representa as imagens que o Usuário poderá anexar na Publicacão.
        /// </summary>
        public virtual IList<Arquivo> Imagens { get; protected set; }
        /// <summary>
        /// Se a Publicação atual for uma resposta, esse atributo irá representar a Publicação alvo da resposta.
        /// </summary>
        public virtual Publicacao PublicacaoRespondida { get; protected set; }
        /// <summary>
        /// Esse atributo irá representar a Publicação alvo do recompartilhamento.
        /// </summary>
        public virtual Publicacao PublicacaoRecompartilhada { get; protected set; }
        /// <summary>
        /// Representa os usuários que deram UpVote na Publicação
        /// </summary>
        public virtual IList<PublicacaoUpvote> UpVotes { get; protected set; }
        /// <summary>
        /// Representa os usuários que deram DownVote na Publicação
        /// </summary>
        public virtual IList<PublicacaoDownvote> DownVotes { get; protected set; }
        /// <summary>
        /// Representa o estado booleano atual da Publicação.
        /// </summary>
        public virtual bool Ativo { get; set; }

        protected Publicacao()
        {

        }

        public Publicacao(int id)
        {
            Id = id;
        }

        // Cadastrar Publicação
        public Publicacao(string conteudo, Usuario usuario)
        {
            Conteudo = conteudo;
            Usuario = usuario;
            QuantidadeVotos = 0;
            Data = DateTime.Now;
            Ativo = true;
            UpVotes = new List<PublicacaoUpvote>();
            DownVotes = new List<PublicacaoDownvote>();
            Imagens = new List<Arquivo>();
        }

        public virtual void AdicionarImagem(Arquivo imagem)
        {
            Imagens.Add(imagem);
        }

        public virtual void Inativar()
        {
            Ativo = false;
        }

        public virtual void Comentar(Publicacao publicacao)
        {
            PublicacaoRespondida = publicacao;
        }

        public virtual void Recompartilhar(Publicacao publicacao)
        {
            PublicacaoRecompartilhada = publicacao;
        }

        public virtual void Upvote()
        {
            QuantidadeVotos += 1;
        }

        public virtual void Downvote()
        {
            QuantidadeVotos -= 1;
        }
    }
}
