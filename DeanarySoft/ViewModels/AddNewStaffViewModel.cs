using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeanarySoft.BuisnessLayer;
using DeanarySoft.DataLayer.DataBaseClasses;
using DeanarySoft.View;

namespace DeanarySoft.ViewModels;

public class AddNewStaffViewModel: INotifyPropertyChanged
{
	public ICommand AddStaff { get; }
	
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
		set { accessLevel = Convert.ToInt16(value); OnPropertyChanged("AccessLevel"); }
	}

	private string? description;
	public string? Description {
		get { return this.description; }
		set { description = value; OnPropertyChanged("Description"); }
	}

	ICollection<Contactphone> contactphones = new List<Contactphone>();
	public virtual ICollection<Contactphone> Contactphones {
		get { return this.contactphones; }
		set { contactphones = value; OnPropertyChanged("Contactphones"); }
	}
	
	
	
	
	
	public bool? DialogResult { get; set; }

	public AddNewStaffViewModel()
	{
		AddStaff = new DelegateCommand(() => AddingStaff());
	}

	public void AddingStaff()
	{
		DialogResult = true;
		var context = Sourse.ConnectingDataBase();
		Staff st = new Staff();
		st.FirstName = FirstName;
		st.LastName = LastName;
		st.Department = Department;
		st.AccessLevel = AccessLevel;
		st.Description = Description;
		context.Staff.Add(st);
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