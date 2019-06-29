using Appinion.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Maps
{
    /// <summary>
    /// Classe que mapeia os atributos da entidade LogTransacao.
    /// </summary>
    public class LogTransacaoMap : ClassMap<LogTransacao>
    {
        public LogTransacaoMap()
        {
            Table("tb_log_transacao");
            Id(x => x.Id).GeneratedBy.Identity().Column("idt_log_transacao").CustomSqlType("int").Not.Nullable();
            Map(x => x.Operacao).Column("opr_log_transacao").CustomSqlType("varchar(100)").Not.Nullable();
            Map(x => x.UsuarioId).Column("idt_usuario_log_transacao").CustomSqlType("varchar(10)").Nullable();
            Map(x => x.Data).Column("dta_log_transacao").CustomSqlType("datetime").Not.Nullable();
            Map(x => x.Objeto).Column("obj_log_transacao").CustomSqlType("varchar(10000)").Not.Nullable();
            Map(x => x.Origem).Column("org_log_transacao").CustomSqlType("varchar(10000)").Not.Nullable();
        }
    }
}
