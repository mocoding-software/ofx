using System;
using System.Collections.Generic;
using System.Linq;

namespace Mocoding.Ofx.Client.Models
{
    /// <summary>
    /// Contains data about transactions of single bank account.
    /// </summary>
    public class AccountTransactions
    {
        public AccountTransactions(decimal currentBalance, IEnumerable<Transaction> collection)
        {
            CurrentBalance = currentBalance;
            Items = collection.ToArray();
        }
        public decimal CurrentBalance { get; private set; }
        public Transaction[] Items { get; private set;}
}

    public class Transaction
    {
        public Transaction(string id, string type, decimal ammount, DateTime datePosted, string description, string memo)
        {
            Memo = memo;
            Description = description;
            DatePosted = datePosted;
            Ammount = ammount;
            Type = type;
            Id = id;
        }

        public string Id { get; private set; }
        public string Type { get; private set; }
        public decimal Ammount { get; private set; }
        public DateTime DatePosted { get; private set; }
        public string Description { get; private set; }
        public string Memo { get; private set; }
    }
}
