using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class Request
{
    public int StaffId { get; set; }

    public int EquipmentId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string? Description { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
