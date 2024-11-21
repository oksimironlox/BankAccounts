using BankAccounts.BankOperations;
using BankAccounts.BankOperations.Model;
using BankAccounts.Saving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Menu
{
    public class MainMenu : Menu
    {
        private const string CreateNewBankAccount = "CreateNewBankAccount";
        private const string OutputAccountBalance = "OutputAccountBalance";
        private const string TransferFunds = "TransferFunds";
        public void DrawMenu(List<MenuItem> menu)
        {
            ConsoleHelper.ClearScreen();
            foreach (MenuItem menuItem in menu)
            {
                Console.BackgroundColor = menuItem.IsSelected
                    ? ConsoleColor.Green
                    : ConsoleColor.Black;

                Console.WriteLine(menuItem.Text);
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void MenuSelectNext(List<MenuItem> menu)
        {
            var selectedItem = menu.First(x => x.IsSelected);
            int selectedIndex = menu.IndexOf(selectedItem);
            selectedItem.IsSelected = false;

            selectedIndex = selectedIndex == menu.Count - 1
                ? 0
                : ++selectedIndex;

            menu[selectedIndex].IsSelected = true;
        }

        public void MenuSelectPrev(List<MenuItem> menu)
        {
            var selectedItem = menu.First(x => x.IsSelected);
            int selectedIndex = menu.IndexOf(selectedItem);
            selectedItem.IsSelected = false;

            selectedIndex = selectedIndex == 0
                ? menu.Count - 1
                : --selectedIndex;

            menu[selectedIndex].IsSelected = true;
        }

        public void Execute(string commandId)
        {
            ConsoleHelper.ClearScreen();
            switch (commandId)
            {
                case CreateNewBankAccount:
                    Bank.AddAccount();
                    DisplayInfo.DisplayAddAccount();
                    break;
                case OutputAccountBalance:
                    AccountsMenu accountsMenu = new AccountsMenu();
                    accountsMenu.Menu(commandId);
                    break;
                case TransferFunds:
                    AccountsMenu accountsMenu2 = new AccountsMenu();
                    accountsMenu2.Menu(commandId);
                    break;
                case "exit":
                    break;
            }
        }

        public void Menu()
        {
            Console.CursorVisible = false;
            List<MenuItem> menu = new List<MenuItem>
            {
                new MenuItem {Id = CreateNewBankAccount, Text = "Создать новый банковский счёт", IsSelected = true},
                new MenuItem {Id = OutputAccountBalance, Text = "Вывести баланс на счёте" },
                new MenuItem {Id = TransferFunds, Text = "Перевести средства между счетами" },
                new MenuItem {Id = "exit", Text = "Выход" }
            };

            bool exit = false;
            do
            {
                DrawMenu(menu);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        MenuSelectNext(menu);
                        break;
                    case ConsoleKey.UpArrow:
                        MenuSelectPrev(menu);
                        break;
                    case ConsoleKey.Enter:
                        var selectedItem = menu.First(x => x.IsSelected);
                        Execute(selectedItem.Id);

                        Console.WriteLine("Хотите продолжить? y/n");
                        string answer = Console.ReadLine();
                        exit = answer == "n" || answer == "no";
                        
                        break;
                }
            } while (!exit);
            XmlBankDataSerializer.SaveBankData();
        }

        public void Menu(string commandId)
        {
            throw new NotImplementedException();
        }
    }
}
