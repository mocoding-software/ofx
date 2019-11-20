using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Interfaces;

namespace Mocoding.Ofx.Client.Defaults
{
    /// <summary>
    /// Default implementation of OFX Client Factory.
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Client.Interfaces.IOfxClientFactory" />
    public class DefaultOfxClientFactory : IOfxClientFactory
    {        
        private readonly IOfxSerializerFactory _serializerFactory;
        private readonly IProtocolUtilsFactory _utilsFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultOfxClientFactory"/> class.
        /// </summary>
        /// <param name="serializerFactory">The serializer factory.</param>
        /// <param name="utilsFactory">The utils factory.</param>
        public DefaultOfxClientFactory(IOfxSerializerFactory serializerFactory, IProtocolUtilsFactory utilsFactory)
        {
            _serializerFactory = serializerFactory;
            _utilsFactory = utilsFactory;
        }

        /// <summary>
        /// Creates <see cref="IOfxClient" /> based on options provided.
        /// </summary>
        /// <param name="options">The OFX options.</param>
        /// <returns>
        /// Concrete OFX client implementation.
        /// </returns>
        public IOfxClient Create(OfxClientOptions options)
        {
            var serializer = _serializerFactory.Create(options.Version);
            var utils = _utilsFactory.Create(options.BankFid);

            return new OfxClient(options, utils, serializer);
        }

        /// <summary>
        /// Default Factory Instance
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public DefaultOfxClientFactory Instance => new DefaultOfxClientFactory(new DefaultOfxSerializerFactory(), new DefaultProtocolUtilsFactory());
    }
}
