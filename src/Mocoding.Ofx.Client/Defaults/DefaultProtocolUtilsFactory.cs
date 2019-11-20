using Mocoding.Ofx.Client.Interfaces;

namespace Mocoding.Ofx.Client.Defaults
{
    /// <summary>
    /// Default implementation for protocol utils factory.
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Client.Interfaces.IProtocolUtilsFactory" />
    public class DefaultProtocolUtilsFactory : IProtocolUtilsFactory
    {
        /// <summary>
        /// Creates <see cref="IProtocolUtils" /> based on specified Financial Institution ID.
        /// </summary>
        /// <param name="fid">Financial Institution ID.</param>
        /// <returns>
        /// Concrete protocol methods implementation.
        /// </returns>
        public IProtocolUtils Create(string fid)
        {
            return new OfxProtocolUtils(new DefaultOfxRequestLocator());
        }
    }
}
