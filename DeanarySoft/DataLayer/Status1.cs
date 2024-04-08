using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer;

public partial class Status1
{
    public int EquipmentId { get; set; }

    public int TypeId { get; set; }

    public DateOnly DateOfAssignment { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual Status Type { get; set; } = null!;
}
