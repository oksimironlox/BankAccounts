using BankAccounts.BankOperations.Model;
using BankAccounts.Menu;
using BankAccounts.Saving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xmlFilePath = "Saving/bankData.xml";
            if (File.Exists(xmlFilePath))
                XmlBankDataSerializer.LoadBankData();
            MainMenu menu = new MainMenu();
            menu.Menu();
        }
    }
}
