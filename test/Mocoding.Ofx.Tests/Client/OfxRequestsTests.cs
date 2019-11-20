using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mocoding.Ofx.Client;
using Mocoding.Ofx.Client.Args;
using Mocoding.Ofx.Client.Defaults;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Models;
using Mocoding.Ofx.Serializers;
using Mocoding.Ofx.Tests;
using NSubstitute;
using Xunit;

namespace Mocoding.Ofx.Tests.Client
{
    public class OfxRequestsTests
    {
        readonly Uri ApiUrl = new Uri("http://localhost:5000/api/ofx");

        [Fact]
        public void CreditCardStatementTest()
        {
            var expectedRequest =
                EmbeddedResourceReader.ReadRequestAsString("creditCardTransactions.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var utilsMock = Substitute.For<IProtocolUtils>();

            utilsMock.Requests.Returns(new DefaultOfxRequestLocator());

            utilsMock.GenerateTransactionId().Returns("0000000000");
            utilsMock.GetCurrentDateTime().Returns("XXXXXXXXXXXXXX");
            utilsMock.DateFormat.Returns(OfxProtocolUtils.DateTimeFormat);
            utilsMock.GetClientUid(Arg.Is("testUserAccount")).Returns("XXXXXXXXX");

            var client = new OfxClient(options, utilsMock, new OfxSgmlSerializer());
            var startDate = new DateTime(2019, 1, 1);
            var endDate = new DateTime(2019, 3, 1);


            var statement = client.PrepareCreditCardStatementOfxRequest(new CreditCardStatementArgs()
            {
                AccountNumber = "XXXXXXXXXXXX3158",
                StartDate = startDate,
                EndDate = endDate
            });

            Assert.Equal(expectedRequest, statement);
        }

        [Fact]
        public void BankStatementTest()
        {
            var expectedRequest =
                EmbeddedResourceReader.ReadRequestAsString("bankTransactions.sgml");

            var options = new OfxClientOptions(ApiUrl, "HAN", "5959", "testUserAccount", "testUserPassword");
            var utilsMock = Substitute.For<IProtocolUtils>();

            utilsMock.Requests.Returns(new DefaultOfxRequestLocator());

            utilsMock.GenerateTransactionId().Returns("0000000000");
            utilsMock.GetCurrentDateTime().Returns("XXXXXXXXXXXXXX");
            utilsMock.DateFormat.Returns(OfxProtocolUtils.DateTimeFormat);
            utilsMock.GetClientUid(Arg.Is("testUserAccount")).Returns("XXXXXXXXX");

            var client = new OfxClient(options, utilsMock, new OfxSgmlSerializer());
            var startDate = new DateTime(2019, 1, 1);
            var endDate = new DateTime(2019, 3, 1);


            var statement = client.PrepareBankStatementOfxRequest(new BankStatementArgs()
            {
                AccountNumber = "YYYYYYYY3158",
                RoutingNumber = "XXXXXXXXX",
                Type = AccountTypeEnum.Checking,
                StartDate = startDate,
                EndDate = endDate
            });

            Assert.Equal(expectedRequest, statement);
        }
    }
}
