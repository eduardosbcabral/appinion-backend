using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Exceptions
{
    public class DownvoteException : Exception
    {
        public DownvoteException(string message)
            : base(message)
        {

        }
    }
}
