using System;
using System.Collections.Generic;
using System.Text;
using Mocoding.Ofx.Models;

namespace Mocoding.Ofx.Client.Args
{
    /// <summary>
    /// Arguments for fetching bank account statement.
    /// </summary>
    public class BankStatementArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementArgs"/> class.
        /// Sets default time range to three months.
        /// </summary>
        public BankStatementArgs()
        {
            StartDate = DateTime.Now.Date.AddMonths(-3);
            EndDate = DateTime.Now.Date;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStatementArgs"/> class from <see cref="Account"/>.
        /// </summary>
        /// <param name="account">The account.</param>
        public BankStatementArgs(Account account) : this()
        {
            AccountNumber = account.Id;
            RoutingNumber = account.BankId;
            Type = account.Type;

        }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the routing number.
        /// </summary>
        /// <value>
        /// The routing number.
        /// </value>
        public string RoutingNumber { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AccountTypeEnum Type { get; set; }

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
