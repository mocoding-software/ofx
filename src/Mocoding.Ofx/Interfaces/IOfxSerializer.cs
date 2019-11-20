using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Interfaces
{
    /// <summary>
    /// Abstraction over serializing and deserializing <seealso cref="OFX"/> model in various protocol versions.
    /// </summary>
    public interface IOfxSerializer
    {
        /// <summary>
        /// Serializes the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Serialized representation.</returns>
        string Serialize(OFX model);

        /// <summary>
        /// Deserializes into model.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>Parsed result - <see cref="OFX" /> Model.</returns>
        OFX Deserialize(string inputString);
    }
}
