
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofiaBoldeskul_Lab2.Tools.Exceptions
{
    internal class BirthException : Exception
    {
        public BirthException(DateTime date) : base($"Error! Entered birthdate {date.ToShortDateString()} is invalid. You don't exist yet!")
        {

        }
    }
}
