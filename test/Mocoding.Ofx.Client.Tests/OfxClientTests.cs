using System;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Mocoding.Ofx;
using Mocoding.Ofx.Client.Exceptions;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Protocol;
using Mocoding.Ofx.Client.Models;
using Mocoding.Ofx.Tests;
using Moq;
using Xunit;

namespace Mocoding.Ofx.Client.Tests
{
    public class OfxClientTests
    {
        readonly Uri ApiUrl = new Uri("http://localhost:5000/api/ofx");

        [Fact]
        public async Task AccountListTest()
        {

            var expectedRequest =
                EmbeddedResourceReader.ReadRequestAsString("accountList.sgml");
            var expectedResponse =
                EmbeddedResourceReader.ReadResponseAsString("accountList.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var transportMock = new Mock<IOfxClientTransport>(); 
            var utilsMock = new Mock<IUtils>();

            transportMock.Setup(t => t.PostRequest(It.IsAny<Uri>(), It.IsAny<string>())).Returns(Task.FromResult(expectedResponse));

            utilsMock.Setup(u => u.GenerateTransactionId()).Returns("0000000000");
            utilsMock.Setup(u => u.GetCurrentDateTime()).Returns("20150127131257");
            utilsMock.Setup(u => u.GetClientUid(It.Is<string>(val => val == "testUserAccount"))).Returns("SomeGuidHere");

            var client = new OfxClient(options, transportMock.Object, utilsMock.Object);
            var result = await client.GetAccounts();
            var account = result;

            Assert.NotEqual(ImmutableArray<Account>.Empty, account);
            Assert.Equal(2, account.Length);
        }

        [Fact]
        public async Task CreditCardTransactionsListTest()
        {

            var expectedRequest =
                EmbeddedResourceReader.ReadRequestAsString("creditCardTransactions.sgml");
            var expectedResponse =
                EmbeddedResourceReader.ReadResponseAsString("creditCardTransactions.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var transportMock = new Mock<IOfxClientTransport>();
            var utilsMock = new Mock<IUtils>();

            transportMock.Setup(t => t.PostRequest(It.IsAny<Uri>(), It.IsAny<string>())).Returns(Task.FromResult(expectedResponse));

            utilsMock.Setup(u => u.GenerateTransactionId()).Returns("0000000000");
            utilsMock.Setup(u => u.GetCurrentDateTime()).Returns("XXXXXXXXXXXXXX");

            var client = new OfxClient(options, transportMock.Object, utilsMock.Object);
            var result = await client.GetTransactions(new Account(AccountTypeEnum.Credit, "XXXXXXXXXXXX3158"));
            var transactions = result;

            Assert.NotEqual(null, transactions);
            Assert.Equal(2, transactions.Items.Length);
        }

        [Fact]
        public async Task BankTransactionsListTest()
        {

            var expectedRequest =
                EmbeddedResourceReader.ReadRequestAsString("bankTransactions.sgml");
            var expectedResponse =
                EmbeddedResourceReader.ReadResponseAsString("bankTransactions.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var transportMock = new Mock<IOfxClientTransport>();
            var utilsMock = new Mock<IUtils>();

            transportMock.Setup(t => t.PostRequest(It.IsAny<Uri>(), It.IsAny<string>())).Returns(Task.FromResult(expectedResponse));

            utilsMock.Setup(u => u.GenerateTransactionId()).Returns("0000000000");
            utilsMock.Setup(u => u.GetCurrentDateTime()).Returns("XXXXXXXXXXXXXX");

            var client = new OfxClient(options, transportMock.Object, utilsMock.Object);
            var result = await client.GetTransactions(new Account(AccountTypeEnum.Checking, "YYYYYYYY1924", "XXXXXXXXX"));
            var transactions = result;

            Assert.NotEqual(null, transactions);
            Assert.Equal(2, transactions.Items.Length);
        }

        [Fact]
        public async Task FailedRequestTest()
        {
            var expectedResponse =
                EmbeddedResourceReader.ReadResponseAsString("error.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var transportMock = new Mock<IOfxClientTransport>();
            var utilsMock = new Mock<IUtils>();

            utilsMock.Setup(u => u.GenerateTransactionId()).Returns("0000000000");
            utilsMock.Setup(u => u.GetCurrentDateTime()).Returns("XXXXXXXXXXXXXX");

            transportMock.Setup(t => t.PostRequest(It.IsAny<Uri>(), It.IsAny<string>())).Returns(Task.FromResult(expectedResponse));

            var client = new OfxClient(options, transportMock.Object, utilsMock.Object);
            var ex = await Assert.ThrowsAsync<OfxResponseException>(() => client.GetAccounts());
            Assert.Equal("An incorrect username/password combination has been entered. Please try again.", ex.Message);
            

        }

        [Fact]
        public async Task ErrorRequestTest()
        {
            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var transportMock = new Mock<IOfxClientTransport>();
            var utilsMock = new Mock<IUtils>();

            utilsMock.Setup(u => u.GenerateTransactionId()).Returns("0000000000");
            utilsMock.Setup(u => u.GetCurrentDateTime()).Returns("XXXXXXXXXXXXXX");

            transportMock.Setup(t => t.PostRequest(It.IsAny<Uri>(), It.IsAny<string>())).Returns(Task.FromResult(string.Empty));

            var client = new OfxClient(options, transportMock.Object, utilsMock.Object);
            var ex = await Assert.ThrowsAsync<FormatException>(() => client.GetAccounts());

            Assert.Equal("<OFX> element is not present in the response body", ex.Message);
        }
    }
}
