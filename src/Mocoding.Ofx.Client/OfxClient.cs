using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Args;
using Mocoding.Ofx.Client.Exceptions;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Client.Requests;
using Mocoding.Ofx.Interfaces;
using Mocoding.Ofx.Models;
using Mocoding.Ofx.Protocol;

[assembly: InternalsVisibleTo("Mocoding.Ofx.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Mocoding.Ofx.Client
{
    /// <summary>
    /// OFX Client Implementation
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Client.Interfaces.IOfxClient" />
    public class OfxClient : IOfxClient
    {
        readonly IOfxSerializer _serializer;
        readonly IProtocolUtils _utils;
        private readonly OfxClientOptions _opts;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfxClient"/> class.
        /// </summary>
        /// <param name="opts">The opts.</param>
        /// <param name="utils">The utils.</param>
        /// <param name="serializer">The serializer.</param>
        public OfxClient(OfxClientOptions opts, IProtocolUtils utils, IOfxSerializer serializer)
        {
            _serializer = serializer;
            _utils = utils;
            _opts = opts;
        }

        /// <summary>
        /// Gets accounts ofx raw payload.
        /// </summary>
        /// <returns>
        /// Raw OFX Payload
        /// </returns>
        public async Task<string> GetAccountsOfx()
        {
            var request = PrepareAccountsOfxRequest();
            var response = await _utils.PostRequest(_opts.ApiUrl, request);

            return response;
        }

        /// <summary>
        /// Gets list of accounts.
        /// </summary>
        /// <returns>
        /// List of bank accounts
        /// </returns>
        public async Task<Account[]> GetAccounts()
        {
            var response = await GetAccountsOfx();
            var ofxPayload = _serializer.Deserialize(response);
            // check for code

            return OfxAccountsParser.Parse(ofxPayload).ToArray();
        }

        /// <summary>
        /// Gets credit card statement ofx payload.
        /// </summary>
        /// <param name="args">Date range and account filter.</param>
        /// <returns>
        /// Raw OFX Payload
        /// </returns>
        public async Task<string> GetStatementOfx(CreditCardStatementArgs args)
        {
            var request = PrepareCreditCardStatementOfxRequest(args);
            var response = await _utils.PostRequest(_opts.ApiUrl, request);

            return response;
        }

        /// <summary>
        /// Gets credit card statement.
        /// </summary>
        /// <param name="args">Date range and account filter.</param>
        /// <returns>
        /// Strongly typed deserialized statement model.
        /// </returns>
        public async Task<Statement> GetStatement(CreditCardStatementArgs args)
        {
            var response = await GetStatementOfx(args);
            var ofxPayload = _serializer.Deserialize(response);
            // check for code

            return OfxStatementParser.Parse(ofxPayload);
        }

        /// <summary>
        /// Gets bank statement ofx payload.
        /// </summary>
        /// <param name="args">Date range and account filter.</param>
        /// <returns>
        /// Raw OFX Payload
        /// </returns>
        public async Task<string> GetStatementOfx(BankStatementArgs args)
        {
            var request = PrepareBankStatementOfxRequest(args);
            var response = await _utils.PostRequest(_opts.ApiUrl, request);

            return response;
        }

        /// <summary>
        /// Gets bank statement ofx payload.
        /// </summary>
        /// <param name="args">Date range and account filter.</param>
        /// <returns>
        /// Strongly typed deserialized statement model.
        /// </returns>
        public async Task<Statement> GetStatement(BankStatementArgs args)
        {
            var response = await GetStatementOfx(args);
            var ofxPayload = _serializer.Deserialize(response);
            // check for code

            return OfxStatementParser.Parse(ofxPayload);
        }

        internal string PrepareAccountsOfxRequest()
        {
            return PrepareOfxRequest(AuthRequest(), GetAccountsRequest());
        }

        internal string PrepareCreditCardStatementOfxRequest(CreditCardStatementArgs args)
        {
            return PrepareOfxRequest(AuthRequest(), GetCreditCardStatementRequest(args));
        }
        internal string PrepareBankStatementOfxRequest(BankStatementArgs args)
        {
            return PrepareOfxRequest(AuthRequest(), GetBankStatementRequest(args));
        }

        internal string PrepareOfxRequest(params IRequestBuilder[] builders)
        {
            var ofxRequest = new OFX() { Items = builders.Select(_ => _.Build()).ToArray() };
            var content = _serializer.Serialize(ofxRequest);

            return content;
        }

        internal IRequestBuilder AuthRequest() => _utils.Requests.GetRequestBuilder<AuthenticateRequestBuilder>()
            .ClientId(_utils.GetClientUid(_opts.UserId))
            .Timestamp(_utils.GetCurrentDateTime())
            .Bank(_opts.BankFid, _opts.BankOrg)
            .Credentials(_opts.UserId, _opts.Password)
            .AppVersion(_opts.AppName, _opts.AppVersion);

        internal IRequestBuilder GetAccountsRequest() => _utils.Requests.GetRequestBuilder<AccountsRequestBuilder>()
            .TransactionId(_utils.GenerateTransactionId());

        internal IRequestBuilder GetCreditCardStatementRequest(CreditCardStatementArgs args) => _utils.Requests.GetRequestBuilder<CreditCardStatementRequestBuilder>()
            .Account(args.AccountNumber)
            .Filter(args.StartDate.ToString(_utils.DateFormat), args.EndDate.ToString(_utils.DateFormat))
            .TransactionId(_utils.GenerateTransactionId());

        internal IRequestBuilder GetBankStatementRequest(BankStatementArgs args) => _utils.Requests.GetRequestBuilder<BankStatementRequestBuilder>()
            .Account(args.AccountNumber, args.RoutingNumber, args.Type.ToString())
            .Filter(args.StartDate.ToString(_utils.DateFormat), args.EndDate.ToString(_utils.DateFormat))
            .TransactionId(_utils.GenerateTransactionId());
    }
}
