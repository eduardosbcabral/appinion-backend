using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class UsuarioQueryModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public string Tipo { get; set; }
        public ArquivoQueryModel Foto { get; set; }
        public IList<SeguidorUsuarioQueryModelUsuarioSeguido> Seguindo { get; set; }
        public IList<SeguidorUsuarioQueryModelUsuarioSeguidor> Seguidores { get; set; }

        public UsuarioQueryModel()
        {
            Seguindo = new List<SeguidorUsuarioQueryModelUsuarioSeguido>();
            Seguidores = new List<SeguidorUsuarioQueryModelUsuarioSeguidor>();
        }

        public int QuantidadeSeguindo {
            get
            {
                return Seguindo.Count;
            }
        }
        public int QuantidadeSeguidores
        {
            get
            {
                return Seguidores.Count;
            }
        }
    }
}
