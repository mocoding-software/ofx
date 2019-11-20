using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Mocoding.Ofx.Interfaces;
using Mocoding.Ofx.Models;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx
{
    /// <summary>
    /// Contains methods to parse OFX payload to get statement and transaction information.
    /// </summary>
    public static class OfxStatementParser
    {
        private const string DateTimeFormat = "yyyyMMddHHmmss";

        /// <summary>
        /// Parses the specified ofx payload and converts it to statement.
        /// Accepts both credit card and bank OFX strings.
        /// </summary>
        /// <param name="ofxPayload">The ofx payload.</param>
        /// <returns>Returns parsed statement.</returns>
        /// <exception cref="Exception">Can't find Credit Card or Bank Statement'.</exception>
        public static Statement Parse(OFX ofxPayload)
        {
            foreach (var messageSet in ofxPayload.Items)
            {
                switch (messageSet)
                {
                    case CreditcardResponseMessageSetV1 cc:
                        return ParseCreditCardStatement(cc.Items.FirstOrDefault() as CreditCardStatementTransactionResponse);
                    case BankResponseMessageSetV1 bank:
                        return ParseBankStatement(bank.Items.FirstOrDefault() as StatementTransactionResponse);
                    default:
                        continue;
                }
            }

            throw new Exception("Can't find Credit Card or Bank Statement'");
        }

        private static Statement ParseBankStatement(StatementTransactionResponse bankStatement)
        {
            var transactions = ParseTransactions(bankStatement.STMTRS.BANKTRANLIST);
            return new Statement()
            {
                AccountNumber = bankStatement.STMTRS.BANKACCTFROM?.ACCTID,
                Currency = bankStatement.STMTRS.CURDEF,
                AvailableBalance = ParseBalance(bankStatement.STMTRS.AVAILBAL?.BALAMT),
                LedgerBalance = ParseBalance(bankStatement.STMTRS.LEDGERBAL?.BALAMT),
                Transactions = transactions,
            };
        }

        private static decimal ParseBalance(string balance)
        {
            if (string.IsNullOrEmpty(balance) || !decimal.TryParse(balance, out var amount))
                amount = 0;

            return amount;
        }

        private static Statement ParseCreditCardStatement(CreditCardStatementTransactionResponse creditCardStatement)
        {
            var transactions = ParseTransactions(creditCardStatement.CCSTMTRS.BANKTRANLIST);
            return new Statement()
            {
                AccountNumber = creditCardStatement.CCSTMTRS.CCACCTFROM?.ACCTID,
                Currency = creditCardStatement.CCSTMTRS.CURDEF,
                AvailableBalance = ParseBalance(creditCardStatement.CCSTMTRS.AVAILBAL?.BALAMT),
                LedgerBalance = ParseBalance(creditCardStatement.CCSTMTRS.LEDGERBAL?.BALAMT),
                Transactions = transactions,
            };
        }

        private static Transaction[] ParseTransactions(BankTransactionList transactionsList)
        {
            return transactionsList?.STMTTRN == null ? new Transaction[0] : transactionsList.STMTTRN.Select(MapToModel).ToArray();
        }

        private static Transaction MapToModel(StatementTransaction transactionDto)
        {
            var amount = decimal.Parse(transactionDto.TRNAMT);

            var truncatedValue = transactionDto.DTPOSTED.Length == DateTimeFormat.Length
                ? transactionDto.DTPOSTED
                : transactionDto.DTPOSTED.Length > DateTimeFormat.Length
                    ? transactionDto.DTPOSTED.Substring(0, DateTimeFormat.Length)
                    : transactionDto.DTPOSTED + new string('0', DateTimeFormat.Length - transactionDto.DTPOSTED.Length);
            var datePosted = DateTime.ParseExact(truncatedValue, DateTimeFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal);

            var description = transactionDto.Item is Payee payee
                ? payee.NAME
                : transactionDto.Item as string;

            return new Transaction()
            {
                Memo = transactionDto.MEMO,
                Description = description,
                DatePosted = datePosted,
                Amount = amount,
                Type = transactionDto.TRNTYPE.ToString(),
                Id = transactionDto.FITID,
            };
        }
    }
}
