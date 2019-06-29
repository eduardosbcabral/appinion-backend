using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Interface
{
    /// <summary>
    /// Interface da classe Repository.
    /// </summary>
    public interface IRepository<T>
    {
        void Save(T obj);
        void SaveOrUpdate(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Delete(object id);
        T Find(Object id);
        T Load(object id);
    }
}
