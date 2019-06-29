﻿using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Repositories
{
    public class OpiniaoRepository : Repository<Opiniao>, IOpiniaoRepository
    {
        public OpiniaoRepository(ISession session)
            : base(session)
        {
                
        }

    }
}
