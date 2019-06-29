using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    /// <summary>
    /// Classe que mapeia os atributos da entidade Publicação.
    /// </summary>
    public class PublicacaoMap : ClassMap<Publicacao>
    {
        public PublicacaoMap()
        {
            Table("tb_publicacao");
            Id(x => x.Id)
                .GeneratedBy.Identity()
                .Column("idt_publicacao")
                .CustomSqlType("int")
                .Not.Nullable();
            Map(x => x.Conteudo)
                .Column("ctd_publicacao")
                .CustomSqlType("varchar(256)")
                .Not.Nullable();
            Map(x => x.QuantidadeVotos)
                .Column("qtd_votos")
                .CustomSqlType("int")
                .Not.Nullable();
            Map(x => x.Data)
                .Column("dta_publicacao")
                .CustomSqlType("datetime")
                .Not.Nullable();
            Map(x => x.Ativo)
                .Column("atv_publicacao");
            References(x => x.Usuario)
                .Column("cod_usuario")
                .ForeignKey("fk_usuario_publicacao")
                .Not.Nullable();
            References(x => x.PublicacaoRespondida)
                .Column("cod_publicacao_respondida")
                .ForeignKey("fk_publicacao_respondida_publicacao")
                .Nullable();
            References(x => x.PublicacaoRecompartilhada)
                .Column("cod_publicacao_recompartilhada")
                .ForeignKey("fk_publicacao_recompartilhada_publicacao")
                .Nullable();
            HasMany(x => x.Imagens)
                .Table("tb_arquivo")
                .KeyColumn("cod_publicacao")
                .Cascade.SaveUpdate();
            HasMany(x => x.UpVotes)
                .Table("tb_publicacao_upvote")
                .KeyColumn("cod_publicacao");
            HasMany(x => x.DownVotes)
                .Table("tb_publicacao_downvote")
                .KeyColumn("cod_publicacao");

        }
    }
}
