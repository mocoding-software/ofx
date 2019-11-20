using System;
using System.Collections.Generic;
using System.Text;

namespace Mocoding.Ofx.Models
{
    /// <summary>
    /// Type of the Account.
    /// </summary>
    public enum AccountTypeEnum
    {
        /// <summary>The checking account</summary>
        Checking = 1,

        /// <summary>The credit account</summary>
        Credit = 2,
    }

    /// <summary>
    /// Contains properties of the bank or credit card account retrieved from OFX.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the account description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the account description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the sub type of the account.
        /// </summary>
        /// <value>
        /// The type of the sub.
        /// </value>
        public string SubType { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AccountTypeEnum Type { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the bank identifier.
        /// </summary>
        /// <value>
        /// The bank identifier.
        /// </value>
        public string BankId { get; set; }

        /// <summary>
        /// Gets or sets the account status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
    }
}