using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.BankOperations.Model
{
    internal class Account
    {
        private static int nextAccountId = 0;
        private int IdAccount { get; set; }
        private int Balance { get; set; }
        public Account() 
        {
            nextAccountId++;
            Balance = 0;
            IdAccount = nextAccountId;
        }

        public Account(int balance, int id)
        {
            Balance = balance;
            IdAccount = id;
        }
        public void setNextAccountId(int nextid)
        {
            nextAccountId = nextid;
        }
        public int getIdAccount()
        {
            return IdAccount;
        }

        public int getBalance()
        {
            return Balance;
        }

        public int getNextAccountId() 
        {
            return nextAccountId;
        }

    }
}
