using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class Equipment : IToStringValue
{
    public int EquipmentId { get; set; }

    public int ModelId { get; set; }

    public DateOnly DeadlinePeriod { get; set; }

    public DateOnly CommissioningDate { get; set; }

    public string? Description { get; set; }

    public virtual Model Model { get; set; } = null!;

    public override string ToString() {
        return $"";
    }

}
