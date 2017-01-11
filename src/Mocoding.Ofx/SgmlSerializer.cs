using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Mocoding.Ofx
{
    public class SgmlSerializer<T>
    {
        private readonly XmlSerializer _serializer;

        public SgmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public string Serialize(T request)
        {
            string result = null;

            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            // Making xml first
            var writer = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true }))
                _serializer.Serialize(xmlWriter, (object)request, ns);

            var xml = writer.ToString();

            // super HACK! :) converting to sgml by removing closing tags for elements with simple value.
            var sgml = Regex.Replace(xml, @"<([A-Za-z0-9_\-\.]+)>([^<]+)</([A-Za-z0-9_\-\.]+)>", "<$1>$2");
            result = Regex.Replace(sgml, @"<[A-Za-z0-9_\-]+ />", string.Empty); // remove empty elements

            return result;
        }

        public T Deserialize(string sgml)
        {
            var xmlDeclaration = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>";

            // converting to xml by adding closing tags for elements with simple value.
            var xml = xmlDeclaration + Regex.Replace(sgml, @"<([A-Za-z0-9_\-\.]+)>([^<]+)", "<$1>$2</$1>");

            // xml part
            var reader = new StringReader(xml);
            var result = (T)_serializer.Deserialize(reader);
            return result;
        }
    }
}
