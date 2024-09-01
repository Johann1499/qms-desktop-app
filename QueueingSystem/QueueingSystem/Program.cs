using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueueingSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            loginPage log = new loginPage();
            CashierOperate cashier = new CashierOperate("Basic Education", "16", "Cashier 4");
            cashier.Show();
            //log.Show();
            Application.Run();
        }
    }
}
