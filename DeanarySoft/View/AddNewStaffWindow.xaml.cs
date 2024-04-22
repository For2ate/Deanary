using System.Windows;
using DeanarySoft.DataLayer.DataBaseClasses;
using DeanarySoft.ViewModels;

namespace DeanarySoft.View;

public partial class AddNewStaffWindow : Window {


	public AddNewStaffWindow() {
		InitializeComponent();
	}

    private void Button_Click(object sender, RoutedEventArgs e) {
		DialogResult = true;
    }
}