using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Client.Requests
{
    /// <summary>Builder Pattern base class.</summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Mocoding.Ofx.Client.Interfaces.IRequestBuilder" />
    public abstract class RequestBuilder<T> : IRequestBuilder where T : class
    {
        private T _requestMessageSet;

        /// <summary>
        /// Creates the request with defaults.
        /// </summary>
        /// <returns></returns>
        protected abstract T CreateRequest();

        /// <summary>
        /// Gets current request instance.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        protected T Request => _requestMessageSet ?? (_requestMessageSet = CreateRequest());

        /// <summary>
        /// Builds the message set to be added to the OFX top level message set collection.
        /// </summary>
        /// <returns></returns>
        public abstract AbstractTopLevelMessageSet Build();
    }
}
