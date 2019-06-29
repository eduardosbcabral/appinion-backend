using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    public class RecompartilharMap : ClassMap<Recompartilhar>
    {
        /// <summary>
        /// Classe que mapeia os atributos da entidade Recompartilhar.
        /// </summary>
        public RecompartilharMap()
        {
            Table("tb_recompartilhar");
            Id(x => x.Id).GeneratedBy.Identity().Column("idt_recompartilhar").CustomSqlType("int").Not.Nullable();
            Map(x => x.Data).Column("dta_recompartilhar").CustomSqlType("datetime").Not.Nullable();
            References(x => x.Usuario).Column("cod_usuario").ForeignKey("fk_usuario_recompartilhar").Not.Nullable();
            References(x => x.Publicacao).Column("cod_publicacao").ForeignKey("fk_publicacao_recompartilhar").Not.Nullable();
        }
    }
}
