using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using DeanarySoft.View;
using DeanarySoft.DataLayer.Context;

namespace DeanarySoft.BuisnessLayer
{

    public class Sourse {

        private static DeanaryContext context = new DeanaryContext();
        private static MainWindow mainWindow = new MainWindow();

        [STAThread()]
        public static void Main() {
            context = ConnectingDataBase();
            App mainApplication = new App();
            mainApplication.Run(mainWindow);
        }

        public static DeanaryContext ConnectingDataBase() {
            //достаем строку подключения
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            DeanaryContext _context = new DeanaryContext(connectionString);
            return _context;
        } 

    }
}
