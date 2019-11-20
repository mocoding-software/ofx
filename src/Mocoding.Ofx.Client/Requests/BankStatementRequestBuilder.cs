using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Client.Requests
{
    /// <summary>
    /// Builds request to fetch bank account statement.
    /// </summary>
    /// <seealso cref="AbstractTransactionRequestBuilder{StatementTransactionRequest}" />
    public class BankStatementRequestBuilder : AbstractTransactionRequestBuilder<StatementTransactionRequest>
    {

        /// <summary>
        /// Sets account information.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="routing">The routing.</param>
        /// <param name="type">The type.</param>
        /// <returns>This instance.</returns>
        public BankStatementRequestBuilder Account(string accountNumber, string routing, string type)
        {
            Request.STMTRQ.BANKACCTFROM = new BankAccount()
            {
                ACCTID = accountNumber,
                BANKID = routing,
                ACCTTYPE = (AccountEnum)Enum.Parse(typeof(AccountEnum), type, true)
            };

            return this;
        }

        /// <summary>
        /// Sets date filter.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>This instance.</returns>
        public BankStatementRequestBuilder Filter(string startDate, string endDate)
        {
            Request.STMTRQ.INCTRAN = new IncTransaction()
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
        protected override StatementTransactionRequest CreateRequest()
        {
            return new StatementTransactionRequest()
            {
                STMTRQ = new StatementRequest()
            };
        }

        /// <summary>
        /// Builds the message set to be added to the OFX top level message set collection.
        /// </summary>
        /// <returns></returns>
        public override AbstractTopLevelMessageSet Build()
        {
            return new BankRequestMessageSetV1()
            {
                Items = new AbstractRequest[]
                {
                    Request
                }
            };
        }
    }
}
