using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.WhosMcu.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
