using Mocoding.Ofx.Client.Defaults;
using Mocoding.Ofx.Client.Interfaces;

namespace Mocoding.Ofx.Client.Discover
{
    /// <summary>
    /// Factory implementation that includes discover specific protocol utils
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Client.Interfaces.IProtocolUtilsFactory" />
    public class DiscoverProtocolUtilsFactory : IProtocolUtilsFactory
    {
        /// <summary>
        /// Creates <see cref="T:Mocoding.Ofx.Client.Interfaces.IProtocolUtils" /> based on specified Financial Institution ID.
        /// </summary>
        /// <param name="fid">Financial Institution ID.</param>
        /// <returns>
        /// Concrete protocol methods implementation.
        /// </returns>
        public IProtocolUtils Create(string fid)
        {
            return fid == "7101" // Discover Credit Card fid
                ? new DiscoverProtocolUtils(new DefaultOfxRequestLocator()) 
                : new OfxProtocolUtils(new DefaultOfxRequestLocator());
        }
    }
}
