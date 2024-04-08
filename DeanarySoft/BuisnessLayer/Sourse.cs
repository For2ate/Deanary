using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanarySoft.BuisnessLayer {

    public class Sourse {

        [STAThread()]
        public static void Main() {
            App mainApplication = new App();
            MainWindow mainWindow = new MainWindow();
            mainApplication.Run(mainWindow);
        }



    }
}
