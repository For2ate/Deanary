using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class Fullrequestinformation {
    public int? IdСотрудника { get; set; }

    public string? ФиСотрудника { get; set; }

    public string? Кафедра { get; set; }

    public short? УровеньДоступа { get; set; }

    public int? Номер { get; set; }

    public int? IdОборудования { get; set; }

    public DateOnly? ДатаВводаВЭксплуотацию { get; set; }

    public string? Производитель { get; set; }

    public string? Модель { get; set; }

    public string? ТипОборудования { get; set; }

    public short? УровеньДоступаОборудования { get; set; }

    public DateOnly? ДатаВыдачи { get; set; }

    public DateOnly? ДатаВозврата { get; set; }
}
