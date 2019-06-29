using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Exceptions
{
    public class UpvoteException : Exception
    {
        public UpvoteException(string message)
            : base(message)
        {

        }
    }
}
