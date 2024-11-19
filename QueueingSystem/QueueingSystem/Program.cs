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
            loginPage login = new loginPage();
            CashierOperate cashier = new CashierOperate("Basic Education", "24", "Cashier 6");
            //cashier.Show();
            
            login.Show();
            //live.Show();
            Application.Run();
        }
    }
}
