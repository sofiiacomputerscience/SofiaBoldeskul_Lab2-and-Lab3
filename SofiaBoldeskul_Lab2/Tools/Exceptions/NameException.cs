using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofiaBoldeskul_Lab2.Tools.Exceptions
{
    internal class NameException : Exception
    {
        public NameException(string name) : base($"Error! Entered name {name} is invalid. You must use a-z letters of both registers.")
        {

        }
    }
}
