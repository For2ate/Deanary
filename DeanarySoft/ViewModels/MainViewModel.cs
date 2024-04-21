using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
using DeanarySoft.BuisnessLayer;
using DeanarySoft.DataLayer;
using DeanarySoft.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DeanarySoft.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private bool _isRequestHistSelected = true;
    private bool _isNewRequestsSelected;
    private bool _isStaffSelected;
    private bool _isEquipmentSelected;
    private readonly DeanaryContext _context;

    public enum DataType { Equipment, Staff, RequestHist, NewRequests, UnSelected}

    
    
    public bool IsRequestHistSelected
    {
        get => _isRequestHistSelected;
        set
        {
            _isRequestHistSelected = value;
            OnPropertyChanged(nameof(IsRequestHistSelected));
        }
    }
    public bool IsNewRequestsSelected
    {
        get => _isNewRequestsSelected;
        set
        {
            _isNewRequestsSelected = value;
            OnPropertyChanged(nameof(IsNewRequestsSelected));
        }
    }
    public bool IsStaffSelected
    {
        get => _isStaffSelected;
        set
        {
            _isStaffSelected = value;
            OnPropertyChanged(nameof(IsStaffSelected));
        }
    }
    public bool IsEquipmentSelected
    {
        get => _isEquipmentSelected;
        set
        {
            _isEquipmentSelected = value;
            OnPropertyChanged(nameof(IsEquipmentSelected));
        }
    }

    
    
    public ObservableCollection<object> Items { get; set; } = new ObservableCollection<object>();
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
        
        _context = Sourse.ConnectingDataBase();
        ShowData(DataType.RequestHist);
        
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
        var history = _context.Requests
            .Select(r => new RequestHistoryViewModel()
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
        var staff = _context.Staff
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
        var equipment = _context.Equipment
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