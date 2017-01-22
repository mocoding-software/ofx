using System;

namespace Mocoding.Ofx.Client.Exceptions
{
    class OfxSerializationException : Exception
    {
        public OfxSerializationException(string message)
            : base(message)
        {

        }
    }
}
