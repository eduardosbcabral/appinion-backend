using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Config.Maps
{
    public class TituloNoticiaMap : SubclassMap<TituloNoticia>
    {
        public TituloNoticiaMap()
        {
            Table("tb_titulo_noticia");
            KeyColumn("idt_titulo_noticia");
            References(x => x.Noticia)
                .Column("cod_noticia")
                .ForeignKey("FK_TB_TITULO_NOTICIA_COD_NOTICIA_TB_TITULO_TB_NOTICIA_IDT_NOTICIA")
                .Not.Nullable();
        }
    }
}
