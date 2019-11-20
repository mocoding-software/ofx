using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mocoding.Ofx.Tests
{
    public static class EmbeddedResourceReader
    {
        public static string ReadAsString(string resourceName)
        {
            var assembly = typeof(EmbeddedResourceReader).GetTypeInfo().Assembly;
            var resourceStream = assembly.GetManifestResourceStream($"Mocoding.Ofx.Tests.TestData.{resourceName}");

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
                return reader.ReadToEnd().Replace("\r\n", "\n");
        }

        public static string ReadRequestAsString(string resourceName) =>
            ReadAsString($"Request.{resourceName}");

        public static string ReadRequestAsString(string resourceName, OfxVersionEnum version) =>
            ReadRequestAsString($"{resourceName}.{GetExetension(version)}");

        public static string ReadResponseAsString(string resourceName) =>
            ReadAsString($"Response.{resourceName}");

        public static string ReadResponseAsString(string resourceName, OfxVersionEnum version) =>
            ReadResponseAsString($"{resourceName}.{GetExetension(version)}");

        public static string GetExetension(OfxVersionEnum version) =>
            version == OfxVersionEnum.Version1x ? "sgml" : "xml";
    }

}
