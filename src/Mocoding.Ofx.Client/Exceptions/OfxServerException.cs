using System;

namespace Mocoding.Ofx.Client.Exceptions
{
    class OfxServerException : Exception
    {
        public OfxServerException(string code, string message)
            : base(message)
        {            
            Code = code;
        }

        public string Code {get; private set;}
    }
}
