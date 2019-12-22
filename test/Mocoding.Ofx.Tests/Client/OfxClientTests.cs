using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Mocoding.Ofx.Client;
using Mocoding.Ofx.Client.Args;
using Mocoding.Ofx.Client.Defaults;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Models;
using Mocoding.Ofx.Serializers;
using NSubstitute;
using Xunit;

namespace Mocoding.Ofx.Tests.Client
{
    public class OfxClientTests
    {
        readonly Uri ApiUrl = new Uri("http://localhost:5000/api/ofx");

        [Fact]
        public async Task AccountListTest()
        {
            var expectedResponse =
                EmbeddedResourceReader.ReadResponseAsString("accountList.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var utilsMock = Substitute.For<IProtocolUtils>();
            
            utilsMock.Requests.Returns(new DefaultOfxRequestLocator());
            utilsMock.PostRequest(Arg.Any<Uri>(), Arg.Any<string>()).Returns(Task.FromResult(expectedResponse));

            
            utilsMock.GenerateTransactionId().Returns("0000000000");
            utilsMock.GetCurrentDateTime().Returns("20150127131257");
            utilsMock.GetClientUid(Arg.Is<string>(val => val == "testUserAccount")).Returns("SomeGuidHere");

            var client = new OfxClient(options, utilsMock, new OfxSgmlSerializer());
            var result = await client.GetAccounts();
            var account = result;

            Assert.NotEqual(ImmutableArray<Account>.Empty, account);
            Assert.Equal(2, account.Length);
        }

        [Fact]
        public async Task ProcessOfxMessageTest()
        {
	        var expectedResponse =
		        EmbeddedResourceReader.ReadResponseAsString("accountList.sgml");

	        var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
	        var utilsMock = Substitute.For<IProtocolUtils>();

	        utilsMock.Requests.Returns(new DefaultOfxRequestLocator());
	        utilsMock.PostRequest(Arg.Any<Uri>(), Arg.Any<string>()).Returns(Task.FromResult(expectedResponse));

            utilsMock.GenerateTransactionId().Returns("0000000000");
	        utilsMock.GetCurrentDateTime().Returns("20150127131257");
	        utilsMock.GetClientUid(Arg.Is<string>(val => val == "testUserAccount")).Returns("SomeGuidHere");

	        var serializer = new OfxSgmlSerializer();
	        var client = new OfxClient(options, utilsMock, serializer);

	        var ofx = serializer.Deserialize(client.PrepareAccountsOfxRequest());

            // reuse account request message
            var resultString = await client.ProcessOfxMessage(ofx.Items[1]);

	        ofx = serializer.Deserialize(resultString);
	        var account = OfxAccountsParser.Parse(ofx).ToArray();

            Assert.NotEqual(ImmutableArray<Account>.Empty, account);
	        Assert.Equal(2, account.Length);
        }

        [Fact]
        public async Task CreditCardTransactionsListTest()
        {
            var expectedResponse =
                EmbeddedResourceReader.ReadResponseAsString("creditCardTransactions.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var utilsMock = Substitute.For<IProtocolUtils>();

            utilsMock.Requests.Returns(new DefaultOfxRequestLocator());
            utilsMock.PostRequest(Arg.Any<Uri>(), Arg.Any<string>()).Returns(Task.FromResult(expectedResponse));

            utilsMock.GenerateTransactionId().Returns("0000000000");
            utilsMock.GetCurrentDateTime().Returns("XXXXXXXXXXXXXX");

            var client = new OfxClient(options, utilsMock, new OfxSgmlSerializer());
            var statement = await client.GetStatement(new CreditCardStatementArgs() { AccountNumber = "XXXXXXXXXXXX3158" });

            Assert.NotNull(statement);
            Assert.Equal(2, statement.Transactions.Length);
        }

        [Fact]
        public async Task BankTransactionsListTest()
        {
            var expectedResponse =
                EmbeddedResourceReader.ReadResponseAsString("bankTransactions.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var utilsMock = Substitute.For<IProtocolUtils>();

            utilsMock.Requests.Returns(new DefaultOfxRequestLocator());
            utilsMock.PostRequest(Arg.Any<Uri>(), Arg.Any<string>()).Returns(Task.FromResult(expectedResponse));

            utilsMock.GenerateTransactionId().Returns("0000000000");
            utilsMock.GetCurrentDateTime().Returns("XXXXXXXXXXXXXX");

            var client = new OfxClient(options, utilsMock, new OfxSgmlSerializer());
            var statement = await client.GetStatement(new BankStatementArgs()
            {
                AccountNumber = "YYYYYYYY3158", RoutingNumber = "XXXXXXXXX", Type = AccountTypeEnum.Checking

            });

            Assert.NotNull(statement);
            Assert.Equal(2, statement.Transactions.Length);
        }

        //[Fact]
        //public async Task FailedRequestTest()
        //{
        //    var expectedResponse =
        //        EmbeddedResourceReader.ReadResponseAsString("error.sgml");

        //    var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
        //    var transportMock = Substitute.For<IOfxClientTransport>();
        //    var utilsMock = Substitute.For<IUtils>();

        //    utilsMock.GenerateTransactionId().Returns("0000000000");
        //    utilsMock.GetCurrentDateTime().Returns("XXXXXXXXXXXXXX");

        //    transportMock.PostRequest(Arg.Any<Uri>(), Arg.Any<string>()).Returns(Task.FromResult(expectedResponse));

        //    var client = new OfxClient(options, transportMock, utilsMock);
        //    var ex = await Assert.ThrowsAsync<OfxResponseException>(() => client.GetAccounts());
        //    Assert.Equal("An incorrect username/password combination has been entered. Please try again.", ex.Message);
        //}

        //[Fact]
        //public async Task ErrorRequestTest()
        //{
        //    var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
        //    var transportMock = Substitute.For<IOfxClientTransport>();
        //    var utilsMock = Substitute.For<IUtils>();

        //    utilsMock.GenerateTransactionId().Returns("0000000000");
        //    utilsMock.GetCurrentDateTime().Returns("XXXXXXXXXXXXXX");

        //    transportMock.PostRequest(Arg.Any<Uri>(), Arg.Any<string>()).Returns(Task.FromResult(string.Empty));

        //    var client = new OfxClient(options, transportMock, utilsMock);
        //    var ex = await Assert.ThrowsAsync<FormatException>(() => client.GetAccounts());

        //    Assert.Equal("<OFX> element is not present in the response body", ex.Message);
        //}

        //[Fact]
        //public async Task ParseCreditCardStatementTest()
        //{
        //    var statement =
        //        EmbeddedResourceReader.ReadResponseAsString("creditCardStatement", OfxVersionEnum.Version1x);

        //    var transactions = OfxClient.ParseCreditCardStatement(statement);

        //    Assert.NotNull(transactions);
        //    Assert.Equal(1, transactions.Items.Length);
        //    Assert.Equal(-20.43m, transactions.CurrentBalance);
        //}

    }
}
