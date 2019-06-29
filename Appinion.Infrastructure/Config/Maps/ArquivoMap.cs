using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    public class ArquivoMap : ClassMap<Arquivo>
    {
        /// <summary>
        /// Classe que mapeia os atributos da entidade Arquivo.
        /// </summary>
        public ArquivoMap()
        {
            Table("tb_arquivo");
            Id(x => x.Id).GeneratedBy.Identity().Column("idt_arquivo").CustomSqlType("int").Not.Nullable();
            Map(x => x.Nome).Column("nme_arquivo").CustomSqlType("varchar(100)").Not.Nullable();
            Map(x => x.Tamanho).Column("tmh_arquivo").CustomSqlType("double").Not.Nullable();
            Map(x => x.Data).Column("dta_arquivo").CustomSqlType("datetime").Not.Nullable();
            Map(x => x.Ativo).Column("atv_arquivo").CustomSqlType("boolean").Not.Nullable();
            Map(x => x.Tipo).Column("tip_arquivo").CustomSqlType("varchar(50)").Not.Nullable();

            References(x => x.Usuario).Column("cod_usuario").ForeignKey("fk_usuario_arquivo").Not.Nullable();
        }
    }
}
