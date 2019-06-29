using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Common
{
    /// <summary>
    /// Classe que abstrai a transação do NHibernate.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ITransaction _transaction;
        public ISession CurrentSession { get; private set; }

        public UnitOfWork(ISession session)
        {
            this.CurrentSession = session;
        }

        public void BeginTransaction()
        {
            _transaction = CurrentSession.BeginTransaction();
        }

        public void Execute()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch (Exception ex)
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
                throw ex;
            }

        }

        public void Dispose()
        {
            if (CurrentSession != null)
            {
                CurrentSession.Dispose();
            }
        }
    }
}
