using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Models;

namespace Mocoding.Ofx.Client.Args
{
    /// <summary>
    /// Arguments for fetching credit card account statement.
    /// </summary>
    public class CreditCardStatementArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardStatementArgs"/> class.
        /// </summary>
        public CreditCardStatementArgs()
        {
            StartDate = DateTime.Now.Date.AddMonths(-3);
            EndDate = DateTime.Now.Date;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardStatementArgs"/> class from <see cref="Account"/>.
        /// </summary>
        /// <param name="account">The account.</param>
        public CreditCardStatementArgs(Account account) : this()
        {
            AccountNumber = account.Id;
        }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get;set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }
    }
}
