using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    /// <summary>
    /// Classe que mapeia os atributos da entidade Usuário.
    /// </summary>
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("tb_usuario");
            Id(x => x.Id).GeneratedBy.Identity().Column("idt_usuario").CustomSqlType("int").Not.Nullable();
            Map(x => x.Username).Unique().Column("usn_usuario").CustomSqlType("varchar(50)").Not.Nullable();
            Map(x => x.Senha).Column("pwd_usuario").CustomSqlType("varchar(128)").Not.Nullable();
            Map(x => x.Email).Column("eml_usuario").CustomSqlType("varchar(100)").Not.Nullable();
            Map(x => x.Nome).Column("nme_usuario").CustomSqlType("varchar(100)").Not.Nullable();
            Map(x => x.Descricao).Column("dsc_usuario").CustomSqlType("varchar(500)").Nullable();
            Map(x => x.DataNascimento).Column("dta_nascimento_usuario").CustomSqlType("datetime").Not.Nullable();
            Map(x => x.Ativo).Column("atv_usuario").CustomSqlType("boolean").Not.Nullable();
            Map(x => x.Tipo).Column("tip_usuario").CustomSqlType("varchar(50)").Not.Nullable();

            References(x => x.Foto).Column("cod_foto").ForeignKey("fk_arquivo_usuario").Nullable().Cascade.All();

            HasMany(x => x.Seguindo)
                .Table("tb_seguidor_usuario")
                .KeyColumn("cd_usuario_seguidor")
                .Cascade.All();

            HasMany(x => x.Seguidores)
                .Table("tb_seguidor_usuario")
                .KeyColumn("cd_usuario_seguido")
                .Cascade.All()
                .Inverse();

            HasMany(x => x.UpVotes)
                .Table("tb_publicacao_upvote")
                .KeyColumn("cod_usuario");

            HasMany(x => x.DownVotes)
                .Table("tb_publicacao_downvote")
                .KeyColumn("cod_usuario");

            HasMany(x => x.Titulos)
                .Table("tb_titulos")
                .KeyColumn("cod_usuario");
        }
    }
}
