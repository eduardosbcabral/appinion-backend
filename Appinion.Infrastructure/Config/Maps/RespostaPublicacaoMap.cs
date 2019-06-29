using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    public class RespostaPublicacaoMap : ClassMap<RespostaPublicacao>
    {
        /// <summary>
        /// Classe que mapeia os atributos da entidade RespostaPublicacao.
        /// </summary>
        public RespostaPublicacaoMap()
        {
            Table("tb_reposta_publicacao");
            Id(x => x.Id).GeneratedBy.Identity().Column("idt_resposta_publicacao").CustomSqlType("int").Not.Nullable();
            References(x => x.UsuarioDestino).Column("cod_usuario_destino").ForeignKey("fk_usuario_resposta").Not.Nullable();
            References(x => x.Publicacao).Column("cod_publicacao").ForeignKey("fk_publicacao_resposta").Not.Nullable();
        }
    }
}
