using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Serializers
{
    /// <summary>
    /// Contains XML serialization/deserialization logic for OFX Version 2.x.
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Serializers.BaseSerializer" />
    public class OfxXmlSerializer : BaseSerializer
    {
        private const string Default211Header = @"<?OFX OFXHEADER=""200"" VERSION=""211"" SECURITY=""NONE"" OLDFILEUID=""NONE"" NEWFILEUID=""NONE""?>";

        /// <summary>
        /// Serializes the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Serialized representation.
        /// </returns>
        public override string Serialize(OFX model)
        {
            var xml = SerializeInternal(model);
            return string.Join("\n", XmlHeader, Default211Header, xml);
        }

        /// <summary>
        /// Deserializes into model.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>Parsed result - <see cref="OFX" /> Model.</returns>
        public override OFX Deserialize(string inputString)
        {
            return DeserializeInternal(inputString);
        }
    }
}
