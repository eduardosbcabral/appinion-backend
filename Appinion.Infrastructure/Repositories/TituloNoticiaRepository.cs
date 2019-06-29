using Appinion.Domain.Entity;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using Appinion.Infrastructure.QueryModels;
using AutoMapper;
using NHibernate;
using System;
using System.Linq;

namespace Appinion.Infrastructure.Repositories
{
    public class TituloNoticiaRepository : Repository<TituloNoticia>, ITituloNoticiaRepository
    {
        public TituloNoticiaRepository(ISession session) : base(session)
        {
        }
    }
}
