using System;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Requests;

namespace Mocoding.Ofx.Client.Interfaces
{
    /// <summary>
    /// Contains methods that are used in OFX protocol during request creation and execution.
    /// </summary>
    public interface IProtocolUtils
    {
        /// <summary>
        /// Gets the current date time in specific OFX format
        /// </summary>
        /// <returns>DateTime formatted string.</returns>
        string GetCurrentDateTime();

        /// <summary>
        /// Generates the OFX transaction identifier.
        /// </summary>
        /// <returns>Transaction ID</returns>
        string GenerateTransactionId();

        /// <summary>
        /// Gets the client uid.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Client ID</returns>
        string GetClientUid(string userId);

        /// <summary>
        /// Gets the specific OFX date format
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        string DateFormat { get; }

        /// <summary>
        /// Creates and executes POST request to specified url with specified body content.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        Task<string> PostRequest(Uri url, string content);

        /// <summary>
        /// Gets the service locator for request builders
        /// </summary>
        /// <value>
        /// The requests.
        /// </value>
        IOfxRequestLocator Requests { get; }
    }
}