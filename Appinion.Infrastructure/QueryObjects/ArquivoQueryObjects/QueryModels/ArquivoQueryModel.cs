using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class ArquivoQueryModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public double Tamanho { get; set; }
        public bool Ativo { get; set; }
    }
}
