using System;
using System.Collections.Generic;
using System.Text;

namespace Mocoding.Ofx.Client.Interfaces
{
    /// <summary>
    /// Abstract factory for <see cref="IOfxClient"/>.
    /// </summary>
    public interface IOfxClientFactory
    {
        /// <summary>
        /// Creates <see cref="IOfxClient"/> based on options provided.
        /// </summary>
        /// <param name="options">The OFX options.</param>
        /// <returns>Concrete OFX client implementation.</returns>
        IOfxClient Create(OfxClientOptions options);
    }
}
