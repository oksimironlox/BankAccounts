using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.BankOperations.Model
{
    internal class Account
    {
        private static int nextAccountId = -1;
        private int IdAccount { get; set; }
        private float Balance { get; set; }
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

        public float getBalance()
        {
            return Balance;
        }

        public int getNextAccountId() 
        {
            return nextAccountId;
        }

        public bool Withdrawal(float sum)
        {
            if(Balance == 0) 
            {
                return false;
            }
            Balance -= sum;
            return true;
        }

        public void Adding(float sum)
        {
            Balance += sum;
        }
    }
}
