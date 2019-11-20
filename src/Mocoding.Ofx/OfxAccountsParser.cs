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
    /// Contains methods to parse OFX payload to get account information.
    /// </summary>
    public static class OfxAccountsParser
    {
        /// <summary>
        /// Parses the ofx payload to get account information.
        /// </summary>
        /// <param name="ofxPayload">The ofx payload.</param>
        /// <returns>Array of accounts.</returns>
        public static IEnumerable<Account> Parse(OFX ofxPayload)
        {
            var messageSet =
                ofxPayload.Items.FirstOrDefault(_ => _ is SignupResponseMessageSetV1) as SignupResponseMessageSetV1;
            var accountsResponse =
                messageSet?.Items.FirstOrDefault(_ => _ is AccountInfoTransactionResponse) as AccountInfoTransactionResponse;

            if (accountsResponse?.ACCTINFORS?.ACCTINFO == null)
                yield break;

            foreach (var accountInfoWrap in accountsResponse.ACCTINFORS?.ACCTINFO)
            {
                foreach (var accountInfo in accountInfoWrap.Items)
                {
                    switch (accountInfo)
                    {
                        case CreditCardAccountInfo cc:
                            yield return ParseCreditCardAccount(cc);
                            break;
                        case BankAccountInfo bank:
                            yield return ParseBankAccount(bank);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        private static Account ParseBankAccount(BankAccountInfo bank)
        {
            return new Account()
            {
                Type = AccountTypeEnum.Checking,
                Id = bank.BANKACCTFROM.ACCTID,
                BankId = bank.BANKACCTFROM.BANKID,
                SubType = bank.BANKACCTFROM.ACCTTYPE.ToString(),
                Status = bank.SVCSTATUS.ToString(),
            };
        }

        private static Account ParseCreditCardAccount(CreditCardAccountInfo cc)
        {
            return new Account()
            {
                Type = AccountTypeEnum.Credit,
                Id = cc.CCACCTFROM.ACCTID,
                Status = cc.SVCSTATUS.ToString(),
            };
        }
    }
}
