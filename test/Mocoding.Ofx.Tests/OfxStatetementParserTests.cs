using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Mocoding.Ofx.Tests
{
    public class OfxStatetementParserTests
    {
        [Fact]
        public void CreditCardAccount()
        {
            // Arrange
            var creditCardStatement =
                EmbeddedResourceReader.ReadResponseAsString("creditCardTransactions", OfxVersionEnum.Version1x);
            var serializer = new DefaultOfxSerializerFactory().Create(OfxVersionEnum.Version1x);
            var ofxPayload = serializer.Deserialize(creditCardStatement);

            // Act
            var statement = OfxStatementParser.Parse(ofxPayload);

            // Assert
            Assert.Equal("XXXXXXXXXXXX3158", statement.AccountNumber);
            Assert.Equal("USD", statement.Currency);
            Assert.Equal(0.00m, statement.AvailableBalance);
            Assert.Equal(0.00m, statement.LedgerBalance);
            Assert.Equal(2, statement.Transactions.Length);
        }

        [Fact]
        public void BankAccount()
        {
            // Arrange
            var creditCardStatement =
                EmbeddedResourceReader.ReadResponseAsString("bankTransactions", OfxVersionEnum.Version1x);
            var serializer = new DefaultOfxSerializerFactory().Create(OfxVersionEnum.Version1x);
            var ofxPayload = serializer.Deserialize(creditCardStatement);

            // Act
            var statement = OfxStatementParser.Parse(ofxPayload);

            // Assert
            Assert.Equal("0000000000003158", statement.AccountNumber);
            Assert.Equal("USD", statement.Currency);
            Assert.Equal(1322.42m, statement.AvailableBalance);
            Assert.Equal(1327.42m, statement.LedgerBalance);
            Assert.Equal(2, statement.Transactions.Length);
        }
    }

}
