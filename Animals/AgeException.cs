using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class AgeException: Exception
    {
        public string? message;
        public AgeException()
        {
        }

        public AgeException(string? message): base()
        {
            this.message = message;
        }

        public AgeException(string? message, Exception? innerException) : base(message, innerException)
        {
            this.message = message;
        }

    }
}
