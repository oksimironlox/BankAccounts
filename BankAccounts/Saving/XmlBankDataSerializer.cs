using BankAccounts.BankOperations.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BankAccounts.Saving
{
    internal static class XmlBankDataSerializer
    {
        private static string xmlFilePath = "bankData.xml";

        public static void SaveBankData()
        {
            try
            {
                XDocument xdoc = new XDocument(
                    new XElement("Bank",
                        from acc in Bank.GetAccounts()
                        select new XElement("Account",
                            new XAttribute("IdAccount", acc.getIdAccount()),
                            new XAttribute("Balance", acc.getBalance()),
                            new XAttribute("nextAccountId", acc.getNextAccountId())
                        )
                    )
                );
                xdoc.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения данных в XML: {ex.Message}");
            }
        }

        public static void LoadBankData()
        {
            if (File.Exists(xmlFilePath))
            {
                try
                {
                    XDocument xdoc = XDocument.Load(xmlFilePath);
                    List<Account> accounts = (from accElement in xdoc.Descendants("Account")
                                              let id = int.Parse(accElement.Attribute("IdAccount").Value)
                                              let balance = int.Parse(accElement.Attribute("Balance").Value)
                                              select new Account (balance, id)).ToList();

                    //Добавляем загруженные счета в Bank
                    foreach (Account account in accounts)
                    {
                        Bank.AddAccount(account);
                        account.setNextAccountId(accounts.Max(a => a.getIdAccount()));
                    }
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки данных из XML: {ex.Message}");
                }
            }
        }
    }

}

