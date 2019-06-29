using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Exceptions
{
    public class UsuarioException : Exception
    {
        public UsuarioException(string source, string message)
            : base(message)
        {
            Source = source;
        }
    }
}
