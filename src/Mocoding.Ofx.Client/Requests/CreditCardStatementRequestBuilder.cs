using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Client.Requests
{
    /// <summary>
    /// Builds request to fetch bank account statement.
    /// </summary>
    /// <seealso cref="AbstractTransactionRequestBuilder{CreditCardStatementTransactionRequest}" />
    public class CreditCardStatementRequestBuilder : AbstractTransactionRequestBuilder<CreditCardStatementTransactionRequest>
    {

        /// <summary>
        /// Sets account information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>This instance.</returns>
        public CreditCardStatementRequestBuilder Account(string id)
        {
            Request.CCSTMTRQ.CCACCTFROM = new CreditCardAccount()
            {
                ACCTID = id
            };

            return this;
        }
        /// <summary>
        /// Sets date filter.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>This instance.</returns>
        public CreditCardStatementRequestBuilder Filter(string startDate, string endDate)
        {
            Request.CCSTMTRQ.INCTRAN = new IncTransaction()
            {
                DTSTART = startDate,
                DTEND = endDate,
                INCLUDE = BooleanType.Y
            };

            return this;
        }

        /// <summary>
        /// Creates the request with defaults.
        /// </summary>
        /// <returns></returns>
        protected override CreditCardStatementTransactionRequest CreateRequest()
        {
            return new CreditCardStatementTransactionRequest()
            {
                CCSTMTRQ = new CreditCardStatementRequest()
            };
        }

        /// <summary>
        /// Builds the message set to be added to the OFX top level message set collection.
        /// </summary>
        /// <returns></returns>
        public override AbstractTopLevelMessageSet Build()
        {
            return new CreditcardRequestMessageSetV1()
            {
                Items = new AbstractTransactionRequest[]
                {
                    Request
                }
            };
        }
    }
}
