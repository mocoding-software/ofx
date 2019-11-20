using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Client.Interfaces
{
    /// <summary>
    /// Abstraction over builder that is responsible for build specific message set of OFX.
    /// </summary>
    /// <remarks>Par of Builder Design Pattern for OFX request.</remarks>
    public interface IRequestBuilder
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>Message Set to be added to OFX request.</returns>
        AbstractTopLevelMessageSet Build();
    }
}
