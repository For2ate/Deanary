using DeanarySoft.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class Staff : INotifyPropertyChanged, IToStringValue {
    public int StaffId { get; set; }
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
        set { accessLevel = value; OnPropertyChanged("AccessLevel"); }
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

    public override string ToString() {
        return $"{FirstName} {LastName}, кафедра: {Department}, уровень доступа: {AccessLevel}, краткие сведения: {Description}";
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
        if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
