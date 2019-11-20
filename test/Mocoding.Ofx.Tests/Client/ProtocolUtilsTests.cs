using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Mocoding.Ofx.Client;
using Mocoding.Ofx.Client.Defaults;
using Mocoding.Ofx.Client.Discover;
using Mocoding.Ofx.Client.Interfaces;
using Xunit;

namespace Mocoding.Ofx.Tests.Client
{
    public class ProtocolUtilsTests
    {
        private IProtocolUtils CreateUtils() => new OfxProtocolUtils(new DefaultOfxRequestLocator());
            
        [Fact]
        public void NotNullTests()
        {
            // arrange
            var utils = CreateUtils();

            // act & assert
            Assert.NotNull(utils.GetCurrentDateTime());
            Assert.NotNull(utils.GenerateTransactionId());
            Assert.NotNull(utils.Requests);
        }

        [Fact]
        public void ClientIdTest()
        {
            // arrange
            var utils = CreateUtils();
            var expectedLength = 32;

            // act
            var actualLength = utils.GetClientUid("test").Length;

            // assert
            Assert.Equal(expectedLength, actualLength);
        }
    }
}
