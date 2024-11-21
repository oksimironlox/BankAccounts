using BankAccounts.BankOperations.Model;
using BankAccounts.BankOperations;
using BankAccounts.Saving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Menu
{
    internal class AccountsMenu : Menu
    {
        private List<Account> accounts = new List<Account>();
        public List<MenuItem> GetIdElements()
        {
            List<Account> menuItemsFromSource = Bank.GetAccounts();

            List<MenuItem> menu = new List<MenuItem>();
            foreach (Account itemAccount in menuItemsFromSource)
            {
                menu.Add(new MenuItem { Id = itemAccount.getIdAccount().ToString(), Text = itemAccount.getIdAccount().ToString(), IsSelected = false });
            }

            menu.Add(new MenuItem { Id = "exit", Text = "Выход", IsSelected = false });
            if (menu.Count > 0) menu[0].IsSelected = true;

            return menu;
        }
        public void DrawMenu(List<MenuItem> menu)
        {
            
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
            {
                if (commandId == "exit")
                {
                    return;
                }
                try
                {
                    DisplayInfo.DisplayAccount(int.Parse(commandId));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обработке атрибута: {ex.Message}");
                }
            }
        }

        public void ExecuteTransferFunds(string commandId)
        {
            ConsoleHelper.ClearScreen();
            
                if (commandId == "exit")
                {
                    return;
                }
            try
            {
                
                Account account = Bank.GetAccounts().FirstOrDefault(a => a.getIdAccount() == int.Parse(commandId));
                if (account == null)
                {
                    Console.WriteLine("Счет с таким ID не найден.");
                    return;
                }

                if(account.getBalance() > 0 || accounts.Count == 1)
                {
                    accounts.Add(account);
                }
                
                if(accounts.Count == 2) 
                {
                    float amount = DisplayInfo.DisplayTransferAmount();
                    while(amount > accounts[0].getBalance()) 
                    {
                        DisplayInfo.DisplayError();
                        amount = DisplayInfo.DisplayTransferAmount();
                    }
                    Bank.Transaction(accounts[0].getIdAccount(), accounts[1].getIdAccount(), amount);
                }
                else
                {
                    List<MenuItem> menu = GetIdElements();
                    MenuItem itemToRemove = menu.FirstOrDefault(x => x.Id == commandId);
                    if (itemToRemove != null) menu.Remove(itemToRemove);
                    if (menu.Count > 0) menu[0].IsSelected = true;

                    bool exit = false;
                    do
                    {
                        ConsoleHelper.ClearScreen();
                        if (accounts.Count == 1)
                        {
                            DisplayInfo.DisplaySelectedAddedAccount(accounts[0].getIdAccount());
                        }
                        if (accounts.Count == 0)
                        {
                            DisplayInfo.DisplayError();
                            DisplayInfo.DisplaySelectWithdrawalAccount();
                        }
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
                                var selectedItem = menu.FirstOrDefault(x => x.IsSelected);
                                if (selectedItem != null) ExecuteTransferFunds(selectedItem.Id);
                                exit = true;
                                break;
                        }
                    } while (!exit);


                    XmlBankDataSerializer.SaveBankData();
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный формат ID счета.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }





    public void Menu(string commandId)
    {
            Console.CursorVisible = false;
            List<MenuItem> menu = GetIdElements();
            
            bool exit = false;
            do
            {
                ConsoleHelper.ClearScreen();
                if (commandId == "TransferFunds")
                {
                    DisplayInfo.DisplaySelectWithdrawalAccount();
                }
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
                        if (commandId == "OutputAccountBalance")
                        {
                            Execute(selectedItem.Id);
                        }
                        else
                        {
                            ExecuteTransferFunds(selectedItem.Id);
                        }
                            

                        exit = true;

                        break;
                }
            } while (!exit);
            XmlBankDataSerializer.SaveBankData();
        }

        public void Menu()
        {
            throw new NotImplementedException();
        }
    }

}
