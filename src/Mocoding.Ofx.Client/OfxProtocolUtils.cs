using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Exceptions;
using Mocoding.Ofx.Client.Interfaces;
using Mocoding.Ofx.Client.Requests;

namespace Mocoding.Ofx.Client
{
    /// <summary>
    /// Default OFX Protocol implementation
    /// </summary>
    /// <seealso cref="Mocoding.Ofx.Client.Interfaces.IProtocolUtils" />
    public class OfxProtocolUtils : IProtocolUtils
    {
        /// <summary>
        /// The date time format
        /// </summary>
        public const string DateTimeFormat = "yyyyMMddHHmmss";

        /// <summary>
        /// Initializes a new instance of the <see cref="OfxProtocolUtils"/> class.
        /// </summary>
        /// <param name="_requests">The requests.</param>
        public OfxProtocolUtils(IOfxRequestLocator _requests)
        {
            this.Requests = _requests;
        }

        /// <summary>
        /// Gets the current date time in specific OFX format
        /// </summary>
        /// <returns>
        /// DateTime formatted string.
        /// </returns>
        public virtual string GetCurrentDateTime()
        {
            return DateTime.Now.ToString(DateTimeFormat);
        }

        /// <summary>
        /// Generates the OFX transaction identifier.
        /// </summary>
        /// <returns>
        /// Transaction ID
        /// </returns>
        public virtual string GenerateTransactionId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets the client uid.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Client ID
        /// </returns>
        public virtual string GetClientUid(string userId)
        {
            var bytes = Encoding.ASCII.GetBytes(userId + "chasebanksucks!").Take(16).ToArray();
            return new Guid(bytes).ToString("N");
        }

        /// <summary>
        /// Gets the specific OFX date format
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        public string DateFormat => DateTimeFormat;

        /// <summary>
        /// Creates and executes POST request to specified url with specified body content.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        /// <exception cref="OfxTransportException">Failed to send request to " + url</exception>
        public virtual async Task<string> PostRequest(Uri url, string content)
        {
            string result = null;

            using (var client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false }) { })
            {
                client.DefaultRequestHeaders.ExpectContinue = false;
                var httpContent = new StringContent(content, Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-ofx");
                var response = await client.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                    throw new OfxTransportException("Failed to send request to " + url);
            }

            return result;
        }

        /// <summary>
        /// Gets the service locator for request builders
        /// </summary>
        /// <value>
        /// The requests.
        /// </value>
        public IOfxRequestLocator Requests { get; }
    }
}
