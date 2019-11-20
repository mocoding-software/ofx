using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Mocoding.Ofx.Interfaces;
using Xunit;

namespace Mocoding.Ofx.Tests
{
    public class OfxSerializerTests
    {
        private readonly IOfxSerializerFactory _factory;
        public OfxSerializerTests()
        {
            _factory = new DefaultOfxSerializerFactory();
        }        

        [Theory]
        [InlineData(OfxVersionEnum.Version1x)]
        [InlineData(OfxVersionEnum.Version2x)]
        public void AccountListRequestTest(OfxVersionEnum version)
        {
            var response = EmbeddedResourceReader.ReadRequestAsString("accountList", version);
            var serializer = _factory.Create(version);
            var ofx = serializer.Deserialize(response);
            var serialized = serializer.Serialize(ofx);

            Assert.Equal(response, serialized);
        }

        [Theory]
        [InlineData(OfxVersionEnum.Version1x)]
        [InlineData(OfxVersionEnum.Version2x)]
        public void AccountListResponseTest(OfxVersionEnum version)
        {
            var response = EmbeddedResourceReader.ReadResponseAsString("accountList", version);
            var serializer = _factory.Create(version);
            var ofx = serializer.Deserialize(response);
            var serialized = serializer.Serialize(ofx);

            Assert.Equal(response, serialized);
        }        
    }
}
