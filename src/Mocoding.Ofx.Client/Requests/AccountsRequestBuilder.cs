using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Client.Requests
{
    /// <summary>
    /// Creates request to fetch list of accounts.
    /// </summary>
    /// <seealso cref="AbstractTransactionRequestBuilder{AccountInfoTransactionRequest}" />
    public class AccountsRequestBuilder : AbstractTransactionRequestBuilder<AccountInfoTransactionRequest>
    {

        /// <summary>
        /// Creates the request with defaults.
        /// </summary>
        /// <returns>This instance.</returns>
        protected override AccountInfoTransactionRequest CreateRequest()
        {
            return new AccountInfoTransactionRequest()
            {
                CLTCOOKIE = "4",
                ACCTINFORQ = new AccountInfoRequest() {DTACCTUP = "19900101"}
            };
        }

        /// <summary>
        /// Builds the message set to be added to the OFX top level message set collection.
        /// </summary>
        /// <returns>This instance.</returns>
        public override AbstractTopLevelMessageSet Build()
        {
            return new SignupRequestMessageSetV1()
            {
                Items = new AbstractRequest[]
                {
                    Request
                }
            };
        }
    }
}
