using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    public class OpiniaoMap : ClassMap<Opiniao>
    {
        /// <summary>
        /// Classe que mapeia os atributos da entidade Opinião.
        /// </summary>
        public OpiniaoMap()
        {
            Table("tb_opiniao");
            Id(x => x.Id).GeneratedBy.Identity().Column("idt_opiniao").CustomSqlType("int").Not.Nullable();
            Map(x => x.Tipo).Column("tipo_opiniao").CustomSqlType("varchar(50)").Not.Nullable();
            Map(x => x.Data).Column("dta_opiniao").Not.Nullable();
            References(x => x.Noticia).Column("cod_noticia").ForeignKey("fk_noticia_opiniao").Not.Nullable();
            References(x => x.Usuario).Column("cod_usuario").ForeignKey("fk_usuario_opiniao").Not.Nullable();
        }
    }
}
