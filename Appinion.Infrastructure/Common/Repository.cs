using Appinion.Domain.Interface;
using NHibernate;

namespace Appinion.Infrastructure.Common
{
    /// <summary>
    /// Classe que abstrai as funcionalidades básicas do NHibernate.
    /// </summary>
    public abstract class Repository<T> : IRepository<T>
    {
        protected ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public void Save(T obj)
        {
            _session.Save(obj);
        }
        public void SaveOrUpdate(T obj)
        {
            _session.SaveOrUpdate(obj);
        }
        public void Update(T obj)
        {
            _session.Update(obj);
        }
        public void Delete(T obj)
        {
            _session.Delete(obj);
        }
        public void Delete(object id)
        {
            Delete(Find(id));
        }
        public T Find(object id)
        {
            return _session.Get<T>(id);
        }

        public T Load(object id)
        {
            return _session.Load<T>(id);
        }
    }
}
