using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRestSharpCoreLayer.Utils
{
    public class CustomException
    {
        public class StatusCodeMismatchException : Exception 
        { 
            public StatusCodeMismatchException(string message) : base(message) { }            
        }
    }
}
