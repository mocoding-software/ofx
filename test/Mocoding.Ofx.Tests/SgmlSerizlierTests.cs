using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mocoding.Ofx.Tests
{
    public class Data
    {
        public string User { get; set; }
        public int Age { get; set; }
    }

    public class SgmlSerizlierTests
    {
        [Fact]
        public void SerializeTest()
        {
            var serializer = new SgmlSerializer<Data>();

            var data = new Data(){
                User = "Some",
                Age = 13
                };

            var expected = "<Data><User>Some<Age>13</Data>";
            var actual = serializer.Serialize(data);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeserializeTest()
        {
            var serializer = new SgmlSerializer<Data>();
            
            var actual = serializer.Deserialize("<Data><User>Some<Age>13</Data>");

            Assert.Equal("Some", actual.User);
            Assert.Equal(13, actual.Age);
        }
    }
}
