using DeanarySoft.DataLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using DeanarySoft.View;

namespace DeanarySoft.BuisnessLayer {

    public class Sourse {

        private static DeanaryContext context = new DeanaryContext();
        private static MainWindow mainWindow = new MainWindow();

        [STAThread()]
        public static void Main() {
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

        public static void HaveListObjectsFromDataBase(int typeOfObject) {
            var obj = new List<IToStringValue>();
            switch (typeOfObject) {
                case 1: 
                    obj = new List<IToStringValue>(context.Staff.ToList()); break;
                case 2:
                    obj = new List<IToStringValue>(context.Models.ToList());break;
            }
            //mainWindow.FillList(obj);
        }

    }
}
