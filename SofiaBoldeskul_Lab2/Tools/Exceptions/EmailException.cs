
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofiaBoldeskul_Lab2.Tools.Exceptions
{
    internal class EmailException : Exception
    {
        public EmailException(string email) : base($"Error! You entered invalid email: {email}")
        {

        }
    }
}