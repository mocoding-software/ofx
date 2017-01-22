using System;

namespace Mocoding.Ofx.Client
{
    /// <summary>
    /// Options to initialize URL.
    /// </summary>
    public class OfxClientOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfxClientOptions"/> class.
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="bankOrg">The bank org.</param>
        /// <param name="bankFid">The bank fid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        public OfxClientOptions(Uri apiUrl, string bankOrg, string bankFid, string userId, string password)
        {
            Password = password;
            UserId = userId;
            BankFid = bankFid;
            BankOrg = bankOrg;
            ApiUrl = apiUrl;
        }

        /// <summary>
        /// Gets the bank OFX API url 
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public Uri ApiUrl { get; private set; }

        /// <summary>
        /// Gets the bank org.
        /// </summary>
        /// <value>
        /// The bank org in UPPER register.
        /// </value>
        public string BankOrg { get; private set; }

        /// <summary>
        /// Gets the bank fid.
        /// </summary>
        /// <value>
        /// The bank fid.
        /// </value>
        public string BankFid { get; private set; }

        /// <summary>
        /// Gets the user login.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; private set; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; private set; }
    }
}
