using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Input;
using DeanarySoft.BuisnessLayer;
using DeanarySoft.DataLayer.DataBaseClasses;
using DeanarySoft.View;

namespace DeanarySoft.ViewModels;

public class AddNewStaffViewModel: INotifyPropertyChanged
{
	public ICommand AddStaff { get; }
	public ObservableCollection<short> AccessLevels { get; } = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	public ObservableCollection<Contactphone> Contacts { get; } = new ObservableCollection<Contactphone>();
	private string firstName = null!;
	public string FirstName {
		get { return this.firstName; }
		set { firstName = value; OnPropertyChanged("FirstName"); }
	}
	private string lastName = null!;
	public string LastName {
		get { return this.lastName; }
		set { lastName = value; OnPropertyChanged("LastName"); }
	}
	private string department = null!;
	public string Department {
		get { return this.department; }
		set { department = value; OnPropertyChanged("Department"); }
	}
	private short accessLevel;
	public short AccessLevel {
		get { return this.accessLevel; }
		set { accessLevel = (short)value; OnPropertyChanged("AccessLevel"); }
	}

	private string? description;
	public string? Description {
		get { return this.description; }
		set { description = value; OnPropertyChanged("Description"); }
	}
	public ICommand AddContactCommand { get; }
	public ICommand RemoveContactCommand { get; }
	
	
	private void AddContact()
	{
		Contacts.Add(new Contactphone());
		
	}

	private void RemoveContact(Contactphone contact)
	{
		Contacts.Remove(contact);
	}
	
	
	
	
	
	
	public bool? DialogResult { get; set; }

	public AddNewStaffViewModel()
	{
		AddContactCommand = new DelegateCommand(AddContact);
		RemoveContactCommand = new DelegateCommand<Contactphone>(RemoveContact);

		AddStaff = new DelegateCommand(() => AddingStaff());
	}

	

	public void AddingStaff()
	{
		DialogResult = true;
		var context = MainViewModel.Context;
		Staff st = new Staff
		{
			FirstName = FirstName,
			LastName = LastName,
			Department = Department,
			AccessLevel = AccessLevel,
			Description = Description,
			Contactphones = Contacts
		};
		context.Staff.Add(st);
		context.SaveChanges();
	}
	


	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
}