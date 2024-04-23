using System;
using System.Collections.Generic;
using DeanarySoft.DataLayer.Interfaces;

namespace DeanarySoft.DataLayer.DataBaseClasses;

public partial class Model : IToStringValue {
    public int ModelId { get; set; }

    public string Manufactor { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public string EquipmentType { get; set; } = null!;

    public short? AccessLevel { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
    public override string ToString() {
        return $"{Manufactor}, {ModelName}, {EquipmentType}";
    }

}
