using System;
using System.Collections.Generic;

namespace DeanarySoft.DataLayer;

public partial class Model
{
    public int ModelId { get; set; }

    public string Manufactor { get; set; } = null!;

    public string Model1 { get; set; } = null!;

    public string EquipmentType { get; set; } = null!;

    public short? AccessLevel { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
