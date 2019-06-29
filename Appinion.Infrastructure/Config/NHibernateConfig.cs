using Appinion.Infrastructure.Maps;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Config
{
    public class NHibernateConfig
    {
        FluentConfiguration configuration;

        private IList<IInterceptor> interceptors;
        public NHibernateConfig(string connectionString)
        {
            interceptors = new List<IInterceptor>();

            configuration = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<UsuarioMap>());
        }

        public ISessionFactory BuildSessionFactory()
        {
            foreach (var interceptor in interceptors)
            {
                configuration.ExposeConfiguration(x => x.SetInterceptor(interceptor));
            }

            return configuration.BuildSessionFactory();
        }

        public void ConfigureCurrentSessionContextClass(CurrentSessionContextClassOption currentSessionContextClassOption)
        {
            configuration.CurrentSessionContext(currentSessionContextClassOption.ToString());

        }

        public void CreateSchema()
        {
            configuration.ExposeConfiguration(c => new SchemaExport(c).Execute(true, true, false))
            .BuildConfiguration();
        }

        public void UpdateSchema()
        {
            configuration.ExposeConfiguration(c => new SchemaUpdate(c).Execute(false, true))
                .BuildConfiguration();
        }

        public void ValidateSchema()
        {

            SchemaValidator validator = new SchemaValidator(configuration.BuildConfiguration());
            validator.Validate();

        }

        public void AddInterceptor(IInterceptor interceptor)
        {
            interceptors.Add(interceptor);
        }

    }

    public enum CurrentSessionContextClassOption
    {
        web,
        thread_static
    }
}
