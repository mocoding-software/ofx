using Mocoding.Ofx.Client;
using Mocoding.Ofx.Client.Defaults;
using Mocoding.Ofx.Client.Discover;
using Xunit;

namespace Mocoding.Ofx.Tests.Client
{
    public class DiscoverProtocolUtilsTests
    {
        [Fact]
        public void PrepareDiscoverRequestTest()
        {
            // arrange
            var expected = @"POST / HTTP/1.1
Content-Type: application/x-ofx
Host: server:443
Content-Length: 7
Connection: close

content";

            // act
            var actual = DiscoverProtocolUtils.PrepareRequest("server", 443, "content");

            // assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GetClientUidTest()
        {
            // arrange
            var utils = new DiscoverProtocolUtils(new DefaultOfxRequestLocator());

            // act
            var actual = utils.GetClientUid("test");

            // assert
            Assert.Null(actual);

        }

        [Fact]
        public void DiscoverFactoryTest()
        {
            // arrange
            var factory = new DiscoverProtocolUtilsFactory();
            var expected = typeof(DiscoverProtocolUtils);

            // act
            var actual = factory.Create("7101");

            // assert
            Assert.IsType(expected, actual);

        }

        [Fact]
        public void DefaultDiscoverFactoryTest()
        {
            // arrange
            var factory = new DiscoverProtocolUtilsFactory();
            var expected = typeof(DiscoverProtocolUtils);

            // act
            var actual = factory.Create("test");

            // assert
            Assert.IsNotType(expected, actual);

        }
    }
}
