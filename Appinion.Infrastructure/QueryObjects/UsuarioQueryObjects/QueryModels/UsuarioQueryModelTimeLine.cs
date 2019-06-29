using Appinion.Infrastructure.QueryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryObjects.UsuarioQueryObjects.QueryModels
{
    public class UsuarioQueryModelTimeLine
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public ArquivoQueryModel Foto { get; set; }
    }
}
