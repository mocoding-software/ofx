using System;

namespace Mocoding.Ofx.Client
{
    /// <summary>Options to initialize URL.</summary>
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
        /// <param name="version">OFX Version.</param>
        /// <param name="appName">Application Name</param>
        /// <param name="appVersion">Application Version</param>
        public OfxClientOptions(Uri apiUrl, string bankOrg, string bankFid, string userId, string password, OfxVersionEnum version = OfxVersionEnum.Version1x, string appName = "QWIN", string appVersion = "2500")
        {
            Password = password;
            UserId = userId;
            BankFid = bankFid;
            BankOrg = bankOrg;
            ApiUrl = apiUrl;
            Version = version;
            AppName = appName;
            AppVersion = appVersion;
        }

        /// <summary>
        /// Gets the bank OFX API url 
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public Uri ApiUrl { get; }

        /// <summary>
        /// Gets the bank org.
        /// </summary>
        /// <value>
        /// The bank org in UPPER register.
        /// </value>
        public string BankOrg { get; }

        /// <summary>
        /// Gets the bank fid.
        /// </summary>
        /// <value>
        /// The bank fid.
        /// </value>
        public string BankFid { get; }

        /// <summary>
        /// Gets the user login.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public OfxVersionEnum Version { get; }

        /// <summary>Gets the name of the application.</summary>
        /// <value>The name of the application.</value>
        public string AppName { get; }

        /// <summary>Gets the application version</summary>
        /// <value>The application ver.</value>
        public string AppVersion { get; }
    }
}
