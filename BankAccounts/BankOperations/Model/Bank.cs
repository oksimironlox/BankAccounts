using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.BankOperations.Model
{
    internal static class Bank
    {
        private static List<Account> Accounts = new List<Account>();

        public static List<Account> GetAccounts() 
        {
            return Accounts.ToList();
        }

        public static void AddAccount()
        {
            Account account = new Account();
            Accounts.Add(account);
        }

        public static void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public static Account GetAccount(int id)
        {
            return Accounts[id];
        }

        public static void Transaction(int idWithdrawal, int idAdding, float sum) 
        {
            if(Accounts[idWithdrawal].Withdrawal(sum))
                Accounts[idAdding].Adding(sum);
        }
    }
}
