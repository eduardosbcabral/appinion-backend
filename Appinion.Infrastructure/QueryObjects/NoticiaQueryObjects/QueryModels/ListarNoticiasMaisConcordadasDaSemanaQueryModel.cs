using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class ListarNoticiasMaisConcordadasDaSemanaQueryModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Url { get; set; }
        public string UrlImagem { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
    }
}
