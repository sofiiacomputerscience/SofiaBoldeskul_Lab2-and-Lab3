
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofiaBoldeskul_Lab2.Tools.Exceptions
{
    internal class SurnameException : Exception
    {
        public SurnameException(string surname) : base($"Error! Entered surname {surname} is invalid. You must use a-z letters of both registers.")
        {

        }
    }
}

