using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    public class SeguidorUsuarioMap : ClassMap<SeguidorUsuario>
    {
        /// <summary>
        /// Classe que mapeia os atributos da entidade SeguidorUsuario.
        /// </summary>
        public SeguidorUsuarioMap()
        {
            Table("tb_seguidor_usuario");
            CompositeId()
                .KeyReference(x => x.UsuarioSeguidor, "cd_usuario_seguidor")
                .KeyReference(x => x.UsuarioSeguido, "cd_usuario_seguido");
            Map(x => x.Data).Column("dta_acao").CustomSqlType("datetime").Not.Nullable();
        }
    }

}
