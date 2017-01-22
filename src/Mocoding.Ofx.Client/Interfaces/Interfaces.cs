using System;
using System.Threading.Tasks;

namespace Mocoding.Ofx.Client.Interfaces
{
    /// <summary>
    /// Transport layer for Ofx Client.
    /// </summary>
    interface IOfxClientTransport
    {
        /// <summary>
        /// Creates and executes POST request to specified url with specified body content.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        Task<string> PostRequest(Uri url, string content);
    }


    interface IUtils
    {
        string GetCurrentDateTime();

        string GenerateTransactionId();

        string DateToString(DateTime dateTime);

        string GetClientUid(string userId);
    }
}
