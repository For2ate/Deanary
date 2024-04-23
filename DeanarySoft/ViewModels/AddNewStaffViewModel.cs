using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeanarySoft.DataLayer.DataBaseClasses;
using DeanarySoft.Services;

namespace DeanarySoft.ViewModels;

public class AddNewStaffViewModel: INotifyPropertyChanged
{
	public ICommand AddStaff { get; }
	public ObservableCollection<short> AccessLevels { get; } = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	public ObservableCollection<Contactphone> Contacts { get; } = new ObservableCollection<Contactphone>();
	private IStaffService _staffService;
	
	private string _firstName = null!;
	public string FirstName {
		get => _firstName;
		set { _firstName = value; OnPropertyChanged(); }
	}
	private string _lastName = null!;
	public string LastName {
		get => _lastName;
		set { _lastName = value; OnPropertyChanged(); }
	}
	private string _department = null!;
	public string Department {
		get => _department;
		set { _department = value; OnPropertyChanged(); }
	}
	private short _accessLevel;
	public short AccessLevel {
		get => _accessLevel;
		set { _accessLevel = value; OnPropertyChanged(); }
	}

	private string? _description;
	public string? Description {
		get => _description;
		set { _description = value; OnPropertyChanged(); }
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

	private bool _dialogRes;

	public bool DialogRes
	{
		get => _dialogRes;
		set { _dialogRes = value; OnPropertyChanged(); }
	}


	public AddNewStaffViewModel(IStaffService staffService)
	{
		_staffService = staffService;
		AddContactCommand = new DelegateCommand(AddContact);
		RemoveContactCommand = new DelegateCommand<Contactphone>(RemoveContact);

		AddStaff = new DelegateCommand(() => AddingStaff());
	}

	private Staff _st;
	public Staff St => _st;


	public void AddingStaff()
	{
		DialogRes = true; 
		_st = new Staff
		{
			FirstName = FirstName,
			LastName = LastName,
			Department = Department,
			AccessLevel = AccessLevel,
			Description = Description,
			Contactphones = Contacts
		};
		_staffService.AddStaff(_st);
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