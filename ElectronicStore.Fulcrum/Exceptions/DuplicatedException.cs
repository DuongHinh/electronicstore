using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Fulcrum.Exceptions
{
    public class DuplicatedException : Exception
    {
        public DuplicatedException()
        {

        }

        public DuplicatedException(string message) : base(message)
        {
           
        }

        public DuplicatedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
