using BankAccounts.BankOperations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

        public static void DisplayAccount(int id) 
        {
            Console.WriteLine($"Номер счёта: {Bank.GetAccount(id).getIdAccount()}; Баланс: {Bank.GetAccount(id).getBalance()}.\n");
        }

        public static void DisplaySelectedAddedAccount(int id) 
        {
            Console.WriteLine($"Для списания вы выбрали счёт: {Bank.GetAccount(id).getIdAccount()};\n" +
                $"Текущий баланс: {Bank.GetAccount(id).getBalance()}.\n" +
                $"Выберите счёт пополнения:\n");
        }

        public static void DisplayError()
        {
            Console.WriteLine("На выбранном счету недостаточно средств для списания!");
        }

        public static float DisplayTransferAmount() 
        {
            Console.WriteLine("Введите сумму перевода: ");
            string input = GetAmount();
            while (!float.TryParse(input, out float inputnum))
            {
                Console.WriteLine("Вы ввели некорректную сумму!\nВведите сумму перевода: ");
                input = GetAmount();
            }
            float amount = float.Parse(input);
            return amount;
        }

        private static string GetAmount()
        {
            string amount = Console.ReadLine();
            return amount;  
        }

        public static void DisplaySelectWithdrawalAccount()
        {
            Console.WriteLine("Выберите счет списания:");
        }
    }
}
