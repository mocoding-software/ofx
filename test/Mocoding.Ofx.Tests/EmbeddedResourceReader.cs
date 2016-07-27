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
                return reader.ReadToEnd();
        }

        public static string ReadRequestAsString(string resourceName)
        {
            return ReadAsString($"Request.{resourceName}");
        }

        public static string ReadResponseAsString(string resourceName)
        {
            return ReadAsString($"Response.{resourceName}");
        }
    }

}
