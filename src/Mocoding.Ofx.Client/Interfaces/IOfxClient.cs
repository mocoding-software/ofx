using System;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Args;
using Mocoding.Ofx.Models;

namespace Mocoding.Ofx.Client.Interfaces
{
    /// <summary>
    /// Contains method to exchange information with Financial Institution
    /// using OFX protocol over the network.
    /// </summary>
    public interface IOfxClient
    {
        /// <summary>
        /// Gets accounts ofx raw payload.
        /// </summary>
        /// <returns>Raw OFX Payload</returns>
        Task<string> GetAccountsOfx();
        
        /// <summary>
        /// Gets list of accounts.
        /// </summary>
        /// <returns>List of bank accounts</returns>
        Task<Account[]> GetAccounts();

        /// <summary>
        /// Gets credit card statement ofx payload.
        /// </summary>
        /// <param name="args">Date range and account filter.</param>
        /// <returns>Raw OFX Payload</returns>
        Task<string> GetStatementOfx(CreditCardStatementArgs args);

        /// <summary>
        /// Gets credit card statement.
        /// </summary>
        /// <param name="args">Date range and account filter.</param>
        /// <returns>Strongly typed deserialized statement model.</returns>
        Task<Statement> GetStatement(CreditCardStatementArgs args);

        /// <summary>
        /// Gets bank statement ofx payload.
        /// </summary>
        /// <param name="args">Date range and account filter.</param>
        /// <returns>Raw OFX Payload</returns>
        Task<string> GetStatementOfx(BankStatementArgs args);

        /// <summary>
        /// Gets bank statement ofx payload.
        /// </summary>
        /// <param name="args">Date range and account filter.</param>
        /// <returns>Strongly typed deserialized statement model.</returns>
        Task<Statement> GetStatement(BankStatementArgs args);
    }
}
