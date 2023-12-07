using Entidades.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Exceptions
{
    public class DataBaseManagerException : Exception
    {
        public DataBaseManagerException(string? message) : base(message)
        {
        }

        public DataBaseManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
