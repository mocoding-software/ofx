using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Client.Requests
{
    /// <summary>
    /// Builds request that requires transaction Id.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <seealso cref="Mocoding.Ofx.Client.Requests.RequestBuilder{TRequest}" />
    public abstract class AbstractTransactionRequestBuilder<TRequest> : RequestBuilder<TRequest> where TRequest : AbstractTransactionRequest, new()
    {
        /// <summary>
        /// Sets transaction Id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>This instance.</returns>
        public AbstractTransactionRequestBuilder<TRequest> TransactionId(string id)
        {
            Request.TRNUID = id;

            return this;
        }
    }
}
