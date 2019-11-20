using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Interfaces
{
    /// <summary>
    /// Abstract factory for OFX Serializer based on version.
    /// </summary>
    public interface IOfxSerializerFactory
    {
        /// <summary>
        /// Creates the specified OFX Serializer based on OFX Version.
        /// </summary>
        /// <param name="version">The OFX version.</param>
        /// <returns>Serializer implementation.</returns>
        IOfxSerializer Create(OfxVersionEnum version);
    }
}
