﻿using Appinion.Infrastructure.QueryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryModels
{
    public class DetalharPublicacaoQueryModel
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public int QuantidadeVotos { get; set; }
        public DateTime Data { get; set; }
        public IList<ArquivoQueryModel> Imagens { get; set; }
        public UsuarioQueryModelWithoutList Usuario { get; set; }
        public bool UsuarioDeuUpvote { get; set; }
        public bool UsuarioDeuDownvote { get; set; }
    }
}
