using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Client.Requests;

namespace Mocoding.Ofx.Client.Defaults
{
    /// <summary>
    /// Default implementation of Service Locator for OFX.
    /// Uses Dictionary to store all the request type mappings.
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Client.Interfaces.IOfxRequestLocator" />
    public class DefaultOfxRequestLocator : IOfxRequestLocator
    {
        private Dictionary<Type, Func<object>> _requestBuildersDict;

        /// <summary>
        /// Gets the request builder of the specific type.
        /// </summary>
        /// <typeparam name="TRequestBuilder">The type of the request builder.</typeparam>
        /// <returns>
        /// Request builder implementation.
        /// </returns>
        public TRequestBuilder GetRequestBuilder<TRequestBuilder>() where TRequestBuilder : IRequestBuilder
        {
            return (TRequestBuilder)RequestBuilders[typeof(TRequestBuilder)]();
        }

        /// <summary>
        /// Gets the request builders.
        /// </summary>
        /// <value>
        /// The request builders.
        /// </value>
        protected Dictionary<Type, Func<object>> RequestBuilders => _requestBuildersDict ?? (_requestBuildersDict = InitRequestBuilders());

        /// <summary>
        /// Initializes the request builders.
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<Type, Func<object>> InitRequestBuilders()
        {
            return new Dictionary<Type, Func<object>>()
            {
                {typeof(AuthenticateRequestBuilder), () => new AuthenticateRequestBuilder()},
                {typeof(AccountsRequestBuilder), () => new AccountsRequestBuilder()},
                {typeof(CreditCardStatementRequestBuilder), () => new CreditCardStatementRequestBuilder()},
                {typeof(BankStatementRequestBuilder), () => new BankStatementRequestBuilder()},
            };
        }
    }
}
