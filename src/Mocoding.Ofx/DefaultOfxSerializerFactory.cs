using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Interfaces;
using Mocoding.Ofx.Serializers;

namespace Mocoding.Ofx
{
    /// <summary>
    /// Default implementation for OFX Serializer factory.
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Interfaces.IOfxSerializerFactory" />
    public class DefaultOfxSerializerFactory : IOfxSerializerFactory
    {
        /// <summary>
        /// Creates the specified OFX Serializer based on OFX Version.
        /// </summary>
        /// <param name="version">The OFX version.</param>
        /// <returns>
        /// Serializer implementation.
        /// </returns>
        /// <exception cref="NotSupportedException">Version {version} is not supported. Supported versions are '1' - sgml and '2' - xml.</exception>
        public IOfxSerializer Create(OfxVersionEnum version)
        {
            switch (version)
            {
                case OfxVersionEnum.Version1x:
                    return new OfxSgmlSerializer();
                case OfxVersionEnum.Version2x:
                    return new OfxXmlSerializer();
                default:
                    throw new NotSupportedException($"Version {version} is not supported. Supported versions are '1' - sgml and '2' - xml");
            }
        }
    }
}
