using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Config
{
    public class NHibernateSessionFactory
    {
        public ISessionFactory CreateFactoryWeb(string connectionString)
        {
            var config = new NHibernateConfig(connectionString);
            config.ConfigureCurrentSessionContextClass(CurrentSessionContextClassOption.web);
            config.UpdateSchema();
            return config.BuildSessionFactory();
        }

        public ISessionFactory CreateFactoryUnitTest(string connectionString)
        {
            var config = new NHibernateConfig(connectionString);
            config.ConfigureCurrentSessionContextClass(CurrentSessionContextClassOption.thread_static);

            return config.BuildSessionFactory();
        }
    }
}
