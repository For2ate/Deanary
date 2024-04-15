using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DeanarySoft.BuisnessLayer;

public class MainViewModel : INotifyPropertyChanged
{
	private bool isRequestHistSelected;
	private bool isNewRequestsSelected;
	private bool isStaffSelected;
	private bool isEquipmentSelected;

	public enum DataType { Equipment, Staff, RequestHist, NewRequests }

	public bool IsRequestHistSelected { get => isRequestHistSelected;
		set
		{
			isRequestHistSelected = value;
			OnPropertyChanged(nameof(IsRequestHistSelected));
		}
	}
	public bool IsNewRequestsSelected { get => isNewRequestsSelected;
		set
		{
			isNewRequestsSelected = value;
			OnPropertyChanged(nameof(IsNewRequestsSelected));
		}
	}
	public bool IsStaffSelected { get => isStaffSelected;
		set
		{
			isStaffSelected = value;
			OnPropertyChanged(nameof(IsStaffSelected));
		}
	}
	public bool IsEquipmentSelected { get => isEquipmentSelected;
		set
		{
			isEquipmentSelected = value;
			OnPropertyChanged(nameof(IsEquipmentSelected));
		}
	}
	
	public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();
	public object SelectedItem { get; set; }
	public DataType SelectedDataType { get; set; }

	public ICommand ShowEquipmentCommand { get; }
	public ICommand ShowStaffCommand { get; }
	public ICommand ShowRequestHistoryCommand { get; }
	public ICommand ShowNewRequestsCommand { get; }
	

	public MainViewModel()
	{
		ShowEquipmentCommand = new DelegateCommand(async () => await ShowData(DataType.Equipment));
		ShowStaffCommand = new DelegateCommand(async () => await ShowData(DataType.Staff));
		ShowRequestHistoryCommand = new DelegateCommand(async () => await ShowData(DataType.RequestHist));
		ShowNewRequestsCommand = new DelegateCommand(async () => await ShowData(DataType.NewRequests));
	}

	// Загрузка данных в зависимости от dataType
	// Вместе с вызовом методов заполнения коллекций происходит изменение видимости определенных меню для работы с конкретным отображением
		
	private async Task ShowData(DataType dataType)
	{
		SelectedDataType = dataType;
		Items.Clear();
		SelectedItem = null;

		IsStaffSelected = false;
		IsEquipmentSelected = false;
		IsRequestHistSelected = false;
		IsNewRequestsSelected = false;
	
		// !!!!! Важно !!!!!
		// return необходим для корректной смены меню, до реализации методов
		if (dataType == DataType.Equipment)
		{
			IsEquipmentSelected = true;
			return;

			var equipmentList = await LoadEquipmentAsync();
			foreach (var equipment in equipmentList)
			{
				Items.Add(equipment);
			}
		}
		else if (dataType == DataType.Staff)
		{
			IsStaffSelected = true;
			return;
			var staffList = await LoadStaffAsync();
			foreach (var staff in staffList)
			{
				Items.Add(staff);
			}
		}
		else if (dataType == DataType.RequestHist)
		{
			IsRequestHistSelected = true;
			return;

			var requestHistList = await LoadRequestHistAsync();
			foreach (var request in requestHistList)
			{
				Items.Add(request);
			}
		}
		else if (dataType == DataType.NewRequests)
		{
			IsNewRequestsSelected = true;
			return;
			var newRequestsList = await LoadNewRequestsAsync();
			foreach (var request in newRequestsList)
			{
				Items.Add(request);
			}
		}
	}



	// ... Реализация INotifyPropertyChanged ...
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
	// ... Методы для загрузки данных

	private async Task<IEnumerable> LoadNewRequestsAsync()
	{
		throw new NotImplementedException();
	}
	private async Task<IEnumerable> LoadRequestHistAsync()
	{
		throw new NotImplementedException();
	}
	private async Task<IEnumerable> LoadStaffAsync()
	{
		throw new NotImplementedException();
	}

	private async Task<IEnumerable> LoadEquipmentAsync()
	{
		throw new NotImplementedException();
	}

	
}