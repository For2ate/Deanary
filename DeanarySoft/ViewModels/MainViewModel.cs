using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using DeanarySoft.BuisnessLayer;
using DeanarySoft.DataLayer.Context;
using DeanarySoft.DataLayer.DataBaseClasses;
using DeanarySoft.View;

namespace DeanarySoft.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private bool _isRequestHistSelected = true;
    private bool _isNewRequestsSelected;
    private bool _isStaffSelected;
    private bool _isEquipmentSelected;
    public static readonly DeanaryContext Context = Sourse.ConnectingDataBase();

    public enum DataType { Equipment, Staff, RequestHist, NewRequests, UnSelected}

    public bool IsRequestHistSelected
    {
        get => isRequestHistSelected;
        set
        {
            isRequestHistSelected = value;
            OnPropertyChanged(nameof(IsRequestHistSelected));
        }
    }
    public bool IsNewRequestsSelected
    {
        get => isNewRequestsSelected;
        set
        {
            isNewRequestsSelected = value;
            OnPropertyChanged(nameof(IsNewRequestsSelected));
        }
    }
    public bool IsStaffSelected
    {
        get => isStaffSelected;
        set
        {
            isStaffSelected = value;
            OnPropertyChanged(nameof(IsStaffSelected));
        }
    }
    public bool IsEquipmentSelected
    {
        get => isEquipmentSelected;
        set
        {
            isEquipmentSelected = value;
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

    public ICommand AddNewStaff { get; }

    public MainViewModel() {
        ShowEquipmentCommand = new DelegateCommand(async () => await ShowData(DataType.Equipment));
        ShowStaffCommand = new DelegateCommand(async () => await ShowData(DataType.Staff));
        ShowRequestHistoryCommand = new DelegateCommand(async () => await ShowData(DataType.RequestHist));
        ShowNewRequestsCommand = new DelegateCommand(async () => await ShowData(DataType.NewRequests));
        AddNewStaff = new DelegateCommand(OpenNewStaffDialog);
        
        ShowData(DataType.RequestHist);
        
    }

    private void OpenNewStaffDialog()
    {
        var dialogVM = new AddNewStaffViewModel(new Staff());
        var dialog = new AddNewStaffWindow{ DataContext = dialogVM};
        var currentVM = dialog.DataContext as AddNewStaffViewModel;
        dialog.ShowDialog();
        if (dialog.DialogResult == true) {
            Trace.WriteLine(currentVM.Staff.ToString());
        }
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
            var equipmentList = await LoadEquipmentAsync();
            foreach (var equipment in equipmentList)
            {
                Items.Add(equipment);
            }
        }
        else if (dataType == DataType.Staff)
        {
            IsStaffSelected = true;
            var staffList = await LoadStaffAsync();
            foreach (var staff in staffList)
            {
                Items.Add(staff);
            }
        }
        else if (dataType == DataType.RequestHist)
        {
            IsRequestHistSelected = true;
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

    private async Task<IEnumerable> LoadNewRequestsAsync() {
        throw new NotImplementedException();
    }
    private async Task<IEnumerable> LoadRequestHistAsync()
    {
        var history = Context.Requests
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
            .Select(e => new EquipmentViewModel {
                EquipmentId = e.EquipmentId,
                ModelName = e.Model.ModelName,
                EquipmentType = e.Model.EquipmentType,
                Manufactor = e.Model.Manufactor,
                CommissioningDate = e.CommissioningDate
            })
            .ToList();

        return new ObservableCollection<object>(equipment);
    }

}