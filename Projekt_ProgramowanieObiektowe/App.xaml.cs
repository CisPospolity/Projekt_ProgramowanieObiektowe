using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Projekt_ProgramowanieObiektowe
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Projekt_PO;Integrated Security=True";
        public static TableContext tc;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            tc = new TableContext(connectionString);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            tc.Dispose();
        }

    }
}
