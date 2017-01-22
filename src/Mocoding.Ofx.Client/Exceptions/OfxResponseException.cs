using System;

namespace Mocoding.Ofx.Client.Exceptions
{
    class OfxResponseException : Exception
    {
        public OfxResponseException(string message)
            : base(message)
        {

        }
    }
}
