using System;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Models;

namespace Mocoding.Ofx.Client.Interfaces
{
    public interface IOfxClient
    {
        Task<AccountTransactions> GetTransactions(Account account, TransactionsFilter filter = null);
        Task<Account[]> GetAccounts();
    }

    /// <summary>
    /// Transport layer for Ofx Client.
    /// </summary>
    public interface IOfxClientTransport
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
