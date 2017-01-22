using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mocoding.Ofx.Client.Exceptions;
using Mocoding.Ofx.Client.Models;
using Mocoding.Ofx.Protocol;

namespace Mocoding.Ofx.Client
{
    partial class OfxClient
    {
        public async Task<Account[]> GetAccounts()
        {
            var accountsRequest = new SignupRequestMessageSetV1()
            {
                Items = new AbstractRequest[]
                {
                    new AccountInfoTransactionRequest()
                    {
                       TRNUID = _utils.GenerateTransactionId(),
                       CLTCOOKIE = "4",
                       ACCTINFORQ = new AccountInfoRequest(){DTACCTUP = "19900101000000"}
                    }
                }
            };

            var messageSet =
                await ExecuteRequest<SignupRequestMessageSetV1, SignupResponseMessageSetV1>(accountsRequest);
            var accountsResponse =
                messageSet.Items.FirstOrDefault(_ => _ is AccountInfoTransactionResponse) as AccountInfoTransactionResponse;

            if (accountsResponse == null)
                throw new OfxResponseException("Required response is not present in message set.");

            var result = new List<Account>();
            foreach (var accountInfo in accountsResponse.ACCTINFORS.ACCTINFO)
            {
                AccountTypeEnum type;
                string subtype = null;
                string status = null;
                string accountId = null;
                string bankId = null;

                var bankAccount = accountInfo.Items.FirstOrDefault(_ => _ is BankAccountInfo) as BankAccountInfo;
                var ccAccount =
                    accountInfo.Items.FirstOrDefault(_ => _ is CreditCardAccountInfo) as CreditCardAccountInfo;

                if (bankAccount != null)
                {
                    type = AccountTypeEnum.Checking;
                    accountId = bankAccount.BANKACCTFROM.ACCTID;
                    bankId = bankAccount.BANKACCTFROM.BANKID;
                    subtype = bankAccount.BANKACCTFROM.ACCTTYPE.ToString();
                    status = bankAccount.SVCSTATUS.ToString();
                }
                else if (ccAccount != null)
                {
                    type = AccountTypeEnum.Credit;
                    accountId = ccAccount.CCACCTFROM.ACCTID;
                    status = ccAccount.SVCSTATUS.ToString();
                }
                else
                    continue;

                var account = new Account(type, accountId, bankId, accountInfo.DESC, accountInfo.PHONE, subtype,
                    status);
                result.Add(account);
            }
            return result.ToArray();
        }
    }
}
