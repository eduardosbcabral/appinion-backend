using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class ListarUsuariosMenosParecidosComUsuarioLogadoQueryModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string FotoNome { get; set; }
        public int Quantidade { get; set; }
    }
}
