using System;
using System.Collections.Generic;
using System.Text;

namespace Mocoding.Ofx.Client.Interfaces
{
    /// <summary>
    /// Service locator pattern for accessing specific OFX request builder.
    /// </summary>
    /// <remarks>
    /// This pattern is used to customize request creation per financial institution.
    /// </remarks>
    public interface IOfxRequestLocator
    {
        /// <summary>
        /// Gets the request builder of the specific type.
        /// </summary>
        /// <typeparam name="TRequestBuilder">The type of the request builder.</typeparam>
        /// <returns>Request builder implementation.</returns>
        TRequestBuilder GetRequestBuilder<TRequestBuilder>() where TRequestBuilder : IRequestBuilder;
    }
}
