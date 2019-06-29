using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Config.Maps
{
    public class PublicacaoUpvoteMap : ClassMap<PublicacaoUpvote>
    {
        public PublicacaoUpvoteMap()
        {
            Table("tb_publicacao_upvote");
            CompositeId()
                .KeyReference(x => x.Publicacao, "cod_publicacao")
                .KeyReference(x => x.Usuario, "cod_usuario");

            Map(x => x.Data).Column("dta_acao").CustomSqlType("datetime").Not.Nullable();
        }
    }
}
