using System;
using System.Collections.Generic;
using System.Linq;

namespace Mocoding.Ofx.Models
{
    /// <summary>
    /// Contains data about transactions of single bank account.
    /// </summary>
    public class Statement
    {
        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the ledger balance.
        /// </summary>
        /// <value>
        /// The ledger balance.
        /// </value>
        public decimal LedgerBalance { get; set; }

        /// <summary>
        /// Gets or sets the available balance.
        /// </summary>
        /// <value>
        /// The available balance.
        /// </value>
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Gets or sets the transactions.
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        public Transaction[] Transactions { get; set; }
    }
}
