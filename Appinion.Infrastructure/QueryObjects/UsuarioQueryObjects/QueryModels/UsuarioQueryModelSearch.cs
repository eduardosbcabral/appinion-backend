using Appinion.Infrastructure.QueryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class UsuarioQueryModelSearch
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataNascimento { get; set; }
        public ArquivoQueryModel Foto { get; set; }
    }
}
