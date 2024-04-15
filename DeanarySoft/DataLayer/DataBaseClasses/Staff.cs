using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class Staff : IToStringValue
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public short AccessLevel { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Contactphone> Contactphones { get; set; } = new List<Contactphone>();
    public override string ToString()
    {
        return $"{FirstName} {LastName}, кафедра: {Department}, уровень доступа: {AccessLevel}, краткие сведения: {Description}";
    }
}
