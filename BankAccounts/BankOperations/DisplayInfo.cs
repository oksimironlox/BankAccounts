using BankAccounts.BankOperations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.BankOperations
{
    internal static class DisplayInfo
    {
        public static void DisplayAllAccount()
        {
            List<Account> accounts = Bank.GetAccounts();
            Console.WriteLine("Ваши активные счета:\n");
            foreach (Account account in accounts)
            {
                Console.WriteLine($"Номер счёта: {account.getIdAccount()}; Баланс: {account.getBalance()}.\n");
            }
        }

        public static void DisplayAddAccount()
        {
            Console.WriteLine("Счёт успешно создан!\n");
            DisplayAllAccount();
        }
    }
}
