using Appinion.Infrastructure.QueryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryObjects.UsuarioQueryObjects.QueryModels
{
    public class DetalharUsuarioQueryModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public int QuantidadeSeguindo { get; set; }
        public int QuantidadeSeguidores { get; set; }
        public ArquivoQueryModel Foto { get; set; }
        public TituloUsuarioQueryModel UltimoTitulo { get; set; }
        public bool TemTituloSemanal { get; set; }
    }
}
