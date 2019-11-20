using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Mocoding.Ofx.Interfaces;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Serializers
{
    /// <summary>
    /// Base class for OFX Serializer.
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Interfaces.IOfxSerializer" />
    public abstract class BaseSerializer : IOfxSerializer
    {
        /// <summary>
        /// The default XML header.
        /// </summary>
        public const string XmlHeader = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>";

        private readonly XmlSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSerializer"/> class.
        /// Instantiates internal <see cref="XmlSerializer"/> class.
        /// </summary>
        protected BaseSerializer()
        {
            _serializer = new XmlSerializer(typeof(OFX));
        }

        /// <summary>
        /// Serializes the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Serialized representation.
        /// </returns>
        public abstract string Serialize(OFX model);

        /// <summary>
        /// Deserializes into model.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>Parsed result - <see cref="OFX" /> Model.</returns>
        public abstract OFX Deserialize(string inputString);

        /// <summary>
        /// Uses <see cref="XmlSerializer"/> to serialize OFX Model into xml.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Xml string.</returns>
        protected string SerializeInternal(OFX request)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            var writer = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true }))
                _serializer.Serialize(xmlWriter, (object)request, ns);

            var xml = writer.ToString();

            return xml;
        }

        /// <summary>
        /// Uses <see cref="XmlSerializer"/> to serialize OFX Model from xml.
        /// </summary>
        /// <remarks>This method cuts everything in front of OFX declaration.</remarks>
        /// <param name="input">The input XML string.</param>
        /// <returns>Returns deserialized model.</returns>
        /// <exception cref="FormatException">OFX element is not present in the response body.</exception>
        protected OFX DeserializeInternal(string input)
        {
            // getting root
            var ofxDataStartIndex = input.IndexOf("<OFX>", StringComparison.OrdinalIgnoreCase);
            if (ofxDataStartIndex == -1)
                throw new FormatException("<OFX> element is not present in the response body.");
            var ofxBody = input.Substring(ofxDataStartIndex);

            // appending xml header
            var xml = XmlHeader + ofxBody;

            // xml part
            var reader = new StringReader(xml);
            var result = (OFX)_serializer.Deserialize(reader);
            return result;
        }
    }
}
