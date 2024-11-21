using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts.Menu
{
    static class ConsoleHelper
    {
        public static void ClearScreen()
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new String(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 0);
        }

    }
}
