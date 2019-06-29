using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    public class NoticiaMap : ClassMap<Noticia>
    {
        /// <summary>
        /// Classe que mapeia os atributos da entidade Arquivo.
        /// </summary>
        public NoticiaMap()
        {
            Table("tb_noticia");
            Id(x => x.Id).GeneratedBy.Identity().Column("idt_noticia").CustomSqlType("int").Not.Nullable();
            Map(x => x.Titulo).Column("tit_noticia").CustomSqlType("varchar(500)").Nullable();
            Map(x => x.Url).Column("url_noticia").CustomSqlType("varchar(500)").Nullable();
            Map(x => x.UrlImagem).Column("url_imagem_noticia").CustomSqlType("varchar(500)").Nullable();
            Map(x => x.Data).Column("dta_noticia").CustomSqlType("datetime").Nullable();
            Map(x => x.Conteudo).Column("ctd_noticia").CustomSqlType("varchar(10000)").Nullable();
            Map(x => x.Ativo).Column("atv_noticia").CustomSqlType("boolean");
        }
    }
}
