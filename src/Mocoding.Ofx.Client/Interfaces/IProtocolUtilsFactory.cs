using System;
using System.Collections.Generic;
using System.Text;

namespace Mocoding.Ofx.Client.Interfaces
{
    /// <summary>
    /// Abstract factory for <see cref="IProtocolUtils"/>.
    /// </summary>
    public interface IProtocolUtilsFactory
    {
        /// <summary>
        /// Creates <see cref="IProtocolUtils"/> based on specified Financial Institution ID.
        /// </summary>
        /// <param name="fid">Financial Institution ID.</param>
        /// <returns>Concrete protocol methods implementation.</returns>
        IProtocolUtils Create(string fid);
    }
}
