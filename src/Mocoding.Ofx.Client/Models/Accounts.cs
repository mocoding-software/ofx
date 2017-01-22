namespace Mocoding.Ofx.Client.Models
{
    /// <summary>
    /// Contains data about single user bank account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="type">The account type.</param>
        /// <param name="id">The account identifier.</param>
        /// <param name="bankId">The bank identifier.</param>
        public Account(AccountTypeEnum type, string id, string bankId = null)
        {
            BankId = bankId;
            Id = id;
            Type = type;
        }

        /// <summary>
        /// Internal ctor - to construct object with all properties.
        /// Initializes a new instance of the <see cref="Account" /> class.
        /// </summary>
        /// <param name="type">The account type.</param>
        /// <param name="id">The account identifier.</param>
        /// <param name="bankId">The bank identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="subType">Type of the sub.</param>
        /// <param name="status">The status.</param>
        internal Account(AccountTypeEnum type, string id, string bankId, string description, string phone, string subType, string status)
        {
            Status = status;
            SubType = subType;
            Description = description;
            BankId = bankId;
            Id = id;
            Type = type;
            Phone = phone;
        }

        /// <summary>
        /// Gets the account description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the account description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Phone { get; private set; }

        /// <summary>
        /// Gets the sub type of the account.
        /// </summary>
        /// <value>
        /// The type of the sub.
        /// </value>
        public string SubType { get; private set; }

        /// <summary>
        /// Gets the account type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AccountTypeEnum Type { get; private set; }

        /// <summary>
        /// Gets the account identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the bank identifier.
        /// </summary>
        /// <value>
        /// The bank identifier.
        /// </value>
        public string BankId { get; private set; }

        /// <summary>
        /// Gets the account status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; private set; }
    }

    public enum AccountTypeEnum
    {
        Checking = 1,
        Credit = 2
    }
}
