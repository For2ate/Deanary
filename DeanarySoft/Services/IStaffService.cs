using DeanarySoft.DataLayer.DataBaseClasses;

namespace DeanarySoft.Services;

public interface IStaffService
{
	void AddStaff(Staff staff);
	void DeleteStaff(int staff);
}
