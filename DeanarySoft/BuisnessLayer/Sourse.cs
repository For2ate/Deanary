using DeanarySoft.DataLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeanarySoft.BuisnessLayer {

    public class Sourse {

        private static DeanaryContext context;
        private static MainWindow mainWindow = new MainWindow();

        [STAThread()]
        public static void Main() {
            ConnectingDataBase();
            App mainApplication = new App();
            mainApplication.Run(mainWindow);
        }

        private static void ConnectingDataBase() {
            //достаем строку подключения
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            context = new DeanaryContext(connectionString);
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
