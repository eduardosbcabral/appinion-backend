using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.QueryObjects.Common
{
    public interface IQueryObject<TQueryObject>
    {
        TQueryObject Query { get; }
    }
}
