using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Config.Maps
{
    public class TituloMap : ClassMap<Titulo>
    {
        public TituloMap()
        {
            Table("tb_titulo");
            Id(x => x.Id).GeneratedBy.Identity().Column("idt_titulo").Not.Nullable();
            Map(x => x.Nome).Column("nme_titulo").CustomSqlType("varchar(50)").Not.Nullable();
            Map(x => x.Quantidade).Column("qtd_titulo").CustomSqlType("int").Not.Nullable();
            Map(x => x.Data).Column("dta_titulo").CustomSqlType("datetime").Not.Nullable();
            Map(x => x.Ativo).Column("atv_titulo").Not.Nullable();
        }
    }
}
