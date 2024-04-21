using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DeanarySoft.ViewModels;

public class AddNewStaffViewModel: INotifyPropertyChanged
{
	public ICommand AddStaff { get; }

	public bool? DialogResult { get; set; }

	public AddNewStaffViewModel()
	{
		AddStaff = new DelegateCommand(() => AddingStaff());
	}

	public void AddingStaff()
	{
		DialogResult = true;
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