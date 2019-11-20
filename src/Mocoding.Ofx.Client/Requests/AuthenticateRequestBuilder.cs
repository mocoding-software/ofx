using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Client.Requests
{
    /// <summary>
    /// Creates authentication request.
    /// </summary>
    /// <seealso cref="RequestBuilder{SignonRequest}" />
    public class AuthenticateRequestBuilder : RequestBuilder<SignonRequest>
    {
        /// <summary>
        /// Sets client identifier.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>This instance.</returns>
        public virtual AuthenticateRequestBuilder ClientId(string clientId)
        {
            Request.CLIENTUID = clientId;
            return this;
        }

        /// <summary>
        /// Sets timestamp.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns>This instance.</returns>
        public virtual AuthenticateRequestBuilder Timestamp(string timestamp)
        {
            Request.DTCLIENT = timestamp;
            return this;
        }

        /// <summary>
        /// Sets bank information - fid and org.
        /// </summary>
        /// <param name="fid">The fid.</param>
        /// <param name="org">The org.</param>
        /// <returns>This instance.</returns>
        public virtual AuthenticateRequestBuilder Bank(string fid, string org)
        {
            Request.FI = new FinancialInstitution()
            {
                FID = fid,
                ORG = org
            };
            return this;
        }

        /// <summary>
        /// Sets credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>This instance.</returns>
        public virtual AuthenticateRequestBuilder Credentials(string username, string password)
        {
            Request.Items = new[] {username, password};
            Request.ItemsElementName = new[] {ItemsChoiceType.USERID, ItemsChoiceType.USERPASS};

            return this;
        }

        /// <summary>
        /// Sets application version.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        /// <returns>This instance.</returns>
        public virtual AuthenticateRequestBuilder AppVersion(string name, string version)
        {
            Request.APPID = name;
            Request.APPVER = version;

            return this;
        }

        /// <summary>
        /// Creates the request with defaults.
        /// </summary>
        /// <returns></returns>
        protected override SignonRequest CreateRequest()
        {
            return new SignonRequest()
            {
                LANGUAGE = LanguageEnum.ENG // should we expose this as well?
            };
        }

        /// <summary>
        /// Builds the message set to be added to the OFX top level message set collection.
        /// </summary>
        /// <returns></returns>
        public override AbstractTopLevelMessageSet Build()
        {
            return new SignonRequestMessageSetV1()
            {
                SONRQ = Request
            };
        }
    }
}
