using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using DeanarySoft.BuisnessLayer;
using DeanarySoft.DataLayer;
using DeanarySoft.DataLayer.DataBaseClasses;
using DeanarySoft.Services;
using DeanarySoft.View;
using Microsoft.EntityFrameworkCore;

namespace DeanarySoft.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private bool _isRequestHistSelected = true;
    private bool _isNewRequestsSelected;
    private bool _isStaffSelected;
    private bool _isEquipmentSelected;
    public static readonly DeanaryContext Context = Sourse.ConnectingDataBase();
    private readonly IStaffService _staffService = new StaffService(Context);
    public enum DataType { Equipment, Staff, RequestHist, NewRequests, UnSelected}

    
    
    public bool IsRequestHistSelected
    {
        get => _isRequestHistSelected;
        set
        {
            _isRequestHistSelected = value;
            OnPropertyChanged();
        }
    }
    public bool IsNewRequestsSelected
    {
        get => _isNewRequestsSelected;
        set
        {
            _isNewRequestsSelected = value;
            OnPropertyChanged();
        }
    }
    public bool IsStaffSelected
    {
        get => _isStaffSelected;
        set
        {
            _isStaffSelected = value;
            OnPropertyChanged();
        }
    }
    public bool IsEquipmentSelected
    {
        get => _isEquipmentSelected;
        set
        {
            _isEquipmentSelected = value;
            OnPropertyChanged();
        }
    }

    
    
    public ObservableCollection<object> Items { get; set; } = new ObservableCollection<object>();
    public object SelectedItem { get; set; }
    public DataType SelectedDataType { get; set; }

    public ICommand ShowEquipmentCommand { get; }
    public ICommand ShowStaffCommand { get; }
    public ICommand ShowRequestHistoryCommand { get; }
    public ICommand ShowNewRequestsCommand { get; }

    public ICommand AddNewStaff { get; }
    public ICommand DeleteSelectedStaff { get; }

    public MainViewModel()
    {
        ShowEquipmentCommand = new DelegateCommand(async () => await ShowData(DataType.Equipment));
        ShowStaffCommand = new DelegateCommand(async () => await ShowData(DataType.Staff));
        ShowRequestHistoryCommand = new DelegateCommand(async () => await ShowData(DataType.RequestHist));
        ShowNewRequestsCommand = new DelegateCommand(async () => await ShowData(DataType.NewRequests));
        AddNewStaff = new DelegateCommand(OpenNewStaffDialog);
        DeleteSelectedStaff = new DelegateCommand(DeleteStaff);
        
        ShowData(DataType.RequestHist);
    }

    private void DeleteStaff()
    {
        if (SelectedItem is StaffViewModel staff)
        {
            MessageBoxResult mbr = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Удаление сотрудника", MessageBoxButton.YesNo);
            if (mbr is MessageBoxResult.Yes)
            {
                Items.Remove(staff);
                _staffService.DeleteStaff(staff.StaffId);
            }
        }else {
            MessageBox.Show("Ни один сотрудник не выбран.", "Выбор не сделан", MessageBoxButton.OK);
        }
        
    }
    
    private void OpenNewStaffDialog()
    {
        var dialogVM = new AddNewStaffViewModel(_staffService);
        var dialog = new AddNewStaffWindow { DataContext = dialogVM};
        dialog.ShowDialog();
        if (dialogVM.DialogRes)
        {
            Staff st = dialogVM.St;
            StaffViewModel staff = new StaffViewModel
            {
                Name = st.FirstName + " " + st.LastName,
                AccessLevel = st.AccessLevel,
                Department = st.Department,
                StaffId = st.StaffId
            };
            
            Items.Add(staff);
        }
    }

    // Загрузка данных в зависимости от dataType
    // Вместе с вызовом методов заполнения коллекций происходит изменение видимости определенных меню для работы с конкретным отображением
    // Возможно изменение кода в соответствии с SOLID
    private async Task ShowData(DataType dataType)
    {
        SelectedDataType = dataType;
        Items.Clear();
        SelectedItem = null;
        
        SetSelectedType(SelectedDataType);
        switch (SelectedDataType)
        {
            case DataType.Equipment:
                GetData(await LoadEquipmentAsync());
                break;
            case DataType.Staff:
                GetData(await LoadStaffAsync());
                break;
            case DataType.RequestHist:
                GetData(await LoadRequestHistAsync());
                break;
            case DataType.NewRequests:
            {
                IsNewRequestsSelected = true;
                break;
            }
        }
    }
    
    public void GetData(IEnumerable recivedData)
    {
        foreach (var item in recivedData)
        {
            Items.Add(item);
        }
    }

    public void SetSelectedType(DataType dataType)
    {
        IsEquipmentSelected = false;
        IsStaffSelected = false;
        IsRequestHistSelected = false;
        IsNewRequestsSelected = false;

        switch (dataType)
        {
            case DataType.Equipment:
                IsEquipmentSelected = true;
                break;
            case DataType.Staff:
                IsStaffSelected = true;
                break;
            case DataType.RequestHist:
                IsRequestHistSelected = true;
                break;
            case DataType.NewRequests:
                IsNewRequestsSelected = true;
                break;
        }
    }
   
    // ... Методы для загрузки данных

    private async Task<IEnumerable> LoadNewRequestsAsync()
    {
        throw new NotImplementedException();
    }
    private async Task<IEnumerable> LoadRequestHistAsync()
    {
        var history = Context.Requests
            .Select(r => new RequestHistoryViewModel
            {
                EquipmentId = r.EquipmentId,
                ModelInfo = r.Equipment.Model.ToString(),
                StaffId = r.StaffId,
                StaffName = r.Staff.LastName + " " + r.Staff.FirstName,
                StartDate = r.StartDate,
                ReturnDate = r.ReturnDate
            })
            .ToList();

        return new ObservableCollection<object>(history);
    }
    private async Task<IEnumerable> LoadStaffAsync()
    {
        var staff = Context.Staff
            .Select(s => new StaffViewModel
            {
                StaffId = s.StaffId,
                Name = s.LastName + " " + s.FirstName,
                Department = s.Department,
                AccessLevel = s.AccessLevel
            })
            .ToList();

        return new ObservableCollection<object>(staff);

    }

    private async Task<IEnumerable> LoadEquipmentAsync()
    {
        var equipment = Context.Equipment
            .Include(e => e.Model)
            .Select(e => new EquipmentViewModel
            {
                EquipmentId = e.EquipmentId,
                ModelName = e.Model.ModelName,
                EquipmentType = e.Model.EquipmentType,
                Manufactor = e.Model.Manufactor,
                CommissioningDate = e.CommissioningDate
            })
            .ToList();

        return new ObservableCollection<object>(equipment);
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
}