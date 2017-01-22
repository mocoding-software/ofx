using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Components;
using Mocoding.Ofx.Client.Exceptions;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Protocol;

[assembly:InternalsVisibleTo("Mocoding.Ofx.Client.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Mocoding.Ofx.Client
{
    public partial class OfxClient
    {
        readonly IOfxClientTransport _transport;
        readonly OfxSerializer _serializer;
        readonly IUtils _utils;

        public OfxClient(OfxClientOptions options)
            : this(options, new WebClientTransport(), new OfxSerializer(), new Utils())
        {
            Options = options;
        }

        internal OfxClient(OfxClientOptions options, IOfxClientTransport transport, IUtils utils)
            : this(options, transport, new OfxSerializer(), utils)
        {
        }

        private OfxClient(OfxClientOptions options, IOfxClientTransport transport, OfxSerializer serializer, IUtils utils)
        {
            Options = options;
            _transport = transport;
            _serializer = serializer;
            _utils = utils;
        }

        public OfxClientOptions Options { get;}

        List<AbstractTopLevelMessageSet> CreatedRequest()
        {
            return new List<AbstractTopLevelMessageSet>()
            {
                new SignonRequestMessageSetV1()
                {
                    SONRQ = new SignonRequest()
                    {
                        CLIENTUID = _utils.GetClientUid(Options.UserId),
                        DTCLIENT = _utils.GetCurrentDateTime(),
                        LANGUAGE = LanguageEnum.ENG,
                        FI =
                            new FinancialInstitution()
                            {
                                FID = Options.BankFid,
                                ORG = Options.BankOrg
                            },
                        APPID = "QWIN",
                        APPVER = "2500",
                        Items = new[] {Options.UserId, Options.Password},
                        ItemsElementName = new[] {ItemsChoiceType.USERID, ItemsChoiceType.USERPASS}
                    }
                }
            };

        }

        async Task<TResponseMessage> ExecuteRequest<TRequestMessage, TResponseMessage>(TRequestMessage accountListRequest)
            where TRequestMessage : AbstractRequestMessageSet
            where TResponseMessage : AbstractResponseMessageSet
        {

            var request = CreatedRequest();
            request.Add(accountListRequest);

            var ofxRequest = new OFX() { Items = request.ToArray() };
            var requestBody = _serializer.Serialize(ofxRequest);
            if(requestBody == null)
                throw new OfxSerializationException("Request serialization error.");

            var responseBody = await _transport.PostRequest(Options.ApiUrl, requestBody);
            var ofxResponse = _serializer.Deserialize(responseBody);
            if (ofxResponse == null)
                throw new OfxSerializationException("Response deserialization error.");


            var signInResponse = ofxResponse.Items.FirstOrDefault(_ => _ is SignonResponseMessageSetV1) as SignonResponseMessageSetV1;
            if (signInResponse == null)
                throw new OfxResponseException("SIGNONRESPONSEMESSAGESETV1 is not present in response.");

            if (signInResponse.SONRS.STATUS.CODE != "0")
                throw new OfxResponseException(signInResponse.SONRS.STATUS.MESSAGE);

            var messageSet = ofxResponse.Items.FirstOrDefault(_ => _ is TResponseMessage) as TResponseMessage;

            if (messageSet == null)
                throw new OfxResponseException("Requested message set " + typeof(TResponseMessage).Name.ToUpper() + " is not present in response.");
         
            return messageSet;
        }
    }
}
