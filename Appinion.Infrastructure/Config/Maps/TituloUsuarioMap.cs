using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Config.Maps
{
    public class TituloUsuarioMap : SubclassMap<TituloUsuario>
    {
        public TituloUsuarioMap()
        {
            Table("tb_titulo_usuario");
            KeyColumn("idt_titulo_usuario");
            References(x => x.Usuario)
                .Column("cod_usuario")
                .ForeignKey("FK_TB_TITULO_USUARIO_COD_USUARIO_TB_USUARIO_IDT_USUARIO")
                .Not.Nullable();
        }
    }
}
