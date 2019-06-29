using Appinion.Infrastructure.Common;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryObjects.Common
{
    public class QueryObject
    {
        protected ISession _session { get; private set; }

        public QueryObject(ISession session)
        {
            _session = session;
        }
    }
}
