using System.Text.RegularExpressions;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Serializers
{
    /// <summary>
    /// Contains Sgml serialization/deserialization logic for OFX Version 1.x
    /// Uses XML serialization and some regex magic to get desired result.
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Serializers.BaseSerializer" />
    public class OfxSgmlSerializer : BaseSerializer
    {
        private readonly string[] _default102Headers = new[]
        {
            "OFXHEADER:100",
            "DATA:OFXSGML",
            "VERSION:103",
            "SECURITY:NONE",
            "ENCODING:USASCII",
            "CHARSET:1252",
            "COMPRESSION:NONE",
            "OLDFILEUID:NONE",
            "NEWFILEUID:NONE",
        };

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

            var sgml = Regex.Replace(xml, @"<([A-Za-z0-9_\-\.]+)>([^<]+)</([A-Za-z0-9_\-\.]+)>", "<$1>$2");
            var result = Regex.Replace(sgml, @"<[A-Za-z0-9_\-]+ />", string.Empty);

            return string.Join("\n", string.Join("\n", _default102Headers), string.Empty, result);
        }

        /// <summary>
        /// Deserializes into model.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>Parsed result - <see cref="OFX" /> Model.</returns>
        public override OFX Deserialize(string inputString)
        {
            // convert sgml to xml using Regex
            inputString = inputString.Replace("\r\n", "");

            var xml = Regex.Replace(inputString, @"<([A-Za-z0-9_\-\.]+)>([^<]+)", "<$1>$2</$1>");
            var result = DeserializeInternal(xml);

            return result;
        }
    }
}
