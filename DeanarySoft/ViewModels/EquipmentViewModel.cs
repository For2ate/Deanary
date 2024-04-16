namespace DeanarySoft.ViewModels;

public class EquipmentViewModel
{
    public int EquipmentId { get; set; }
    public DateOnly DeadlinePeriod { get; set; }

    public DateOnly CommissioningDate { get; set; }

    public string? ModelName { get; set; }
    public string? Manufactor { get; set; }
    public string? EquipmentType { get; set; }

    public string? Description { get; set; }

}