using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class Status {
    public int EquipmentId { get; set; }

    public int TypeId { get; set; }

    public DateOnly DateOfAssignment { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual TypeStatus Type { get; set; } = null!;
}
