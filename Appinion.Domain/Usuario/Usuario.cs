using Appinion.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appinion.Domain.Entity
{
    /// <summary>
    /// Representa o Usuário que irá utilizar o sistema.
    /// </summary>
    public class Usuario : EntityBase
    {
        /// <summary>
        /// Representa o username do Usuário.
        /// </summary>
        public virtual string Username { get; protected set; }
        /// <summary>
        /// Representa o e-mail do Usuário.
        /// </summary>
        public virtual string Email { get; protected set; }
        /// <summary>
        /// Representa a senha do Usuário, que será criptografada no hora de salvar.
        /// </summary>
        public virtual string Senha { get; protected set; }
        /// <summary>
        /// Representa o nome do Usuário.
        /// </summary>
        public virtual string Nome { get; protected set; }
        /// <summary>
        /// Representa uma breve descrição do Usuário.
        /// </summary>
        public virtual string Descricao { get; protected set; }
        /// <summary>
        /// Representa a data de nascimento do Usuário.
        /// </summary>
        public virtual DateTime DataNascimento { get; protected set; }
        /// <summary>
        /// Representa o estado booleano do Usuário
        /// </summary>
        public virtual bool Ativo { get; protected set; }
        /// <summary>
        /// Representa os Usuários que o Usuário em questão está seguindo.
        /// </summary>
        public virtual IList<SeguidorUsuario> Seguindo { get; protected set; }
        /// <summary>
        /// Representa os Usuários que seguem o Usuário em questão.
        /// </summary>
        public virtual IList<SeguidorUsuario> Seguidores { get; protected set; }
        /// <summary>
        /// Representa a foto atual do Usuário.
        /// </summary>
        public virtual Arquivo Foto { get; protected set; }
        /// <summary>
        /// Representa o tipo do Usuário.
        /// </summary>
        public virtual TipoUsuario Tipo { get; protected set; }
        /// <summary>
        /// Representa os usuários que deram UpVote na Publicação
        /// </summary>
        public virtual IList<PublicacaoUpvote> UpVotes { get; protected set; }
        /// <summary>
        /// Representa os usuários que deram DownVote na Publicação
        /// </summary>
        public virtual IList<PublicacaoDownvote> DownVotes { get; protected set; }
        public virtual IList<TituloUsuario> Titulos { get; protected set; }
        public virtual bool TemTituloSemanal
        {
            get
            {
                if (!Titulos.Any())
                {
                    return false;
                }

                var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                var dataTitulo = Titulos.OrderBy(x => x.Data).LastOrDefault().Data;
                var d1 = dataTitulo.Date.AddDays(-1 * (int)cal.GetDayOfWeek(dataTitulo));
                var d2 = DateTime.Now.Date.AddDays(-1 * (int)cal.GetDayOfWeek(DateTime.Now));

                return d1 == d2;
            }
        }

        public virtual TituloUsuario UltimoTitulo
        {
            get
            {
                return Titulos.OrderBy(x => x.Data).LastOrDefault();
            }
        }

        protected Usuario()
        {
        }

        public Usuario(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Construtor para o comando CadastrarUsuario
        /// </summary>
        public Usuario(string username, string email, string senha, string nome, DateTime dataNascimento)
        {
            Username = username;
            Email = email;
            Senha = senha;
            Nome = nome;
            DataNascimento = dataNascimento;
            Ativo = true;
            Seguindo = new List<SeguidorUsuario>();
            Seguidores = new List<SeguidorUsuario>();
            Titulos = new List<TituloUsuario>();
            Tipo = TipoUsuario.COMUM;
        }

        public Usuario(string username, string senha)
        {
            Username = username;
            Senha = senha;
        }

        public virtual void AtualizarDescritivo(string nome, string descricao, DateTime dataNascimento)
        {
            Nome = nome;
            Descricao = descricao;
            DataNascimento = dataNascimento;
        }

        public virtual void AtualizarUsername(string username)
        {
            Username = username;
        }

        public virtual void AtualizarEmail(string email)
        {
            Email = email;
        }

        public virtual void AtualizarFoto(Arquivo foto)
        {
            Foto = foto;
        }

        public virtual void AtualizarSenha(string senha)
        {
            Senha = senha;
        }

        public virtual void Inativar()
        {
            Ativo = false;
        }

    }
}
