using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ComidaInvalidaException : Exception
    {
        public ComidaInvalidaException(string? message) : base(message)
        {
        }
    }
}
