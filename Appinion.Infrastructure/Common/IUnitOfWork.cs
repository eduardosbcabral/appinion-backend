using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Common
{
    /// <summary>
    /// Interface da classe UnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Execute();
        void Dispose();
    }
}
