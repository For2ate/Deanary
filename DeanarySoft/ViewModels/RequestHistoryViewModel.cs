namespace DeanarySoft.ViewModels;

public class RequestHistoryViewModel
{
    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public int EquipmentId { get; set; }

    public string ModelInfo { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string? Description { get; set; }

}