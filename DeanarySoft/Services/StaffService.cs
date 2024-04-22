using DeanarySoft.DataLayer;
using DeanarySoft.DataLayer.DataBaseClasses;

namespace DeanarySoft.Services;

public class StaffService(DeanaryContext context) : IStaffService
{
	

	public void AddStaff(Staff staff)
	{
		context.Staff.Add(staff);
		context.SaveChanges();
	}

	public void DeleteStaff(int staffId)
	{
		var staff = context.Staff.Find(staffId);
		if (staff != null)
		{
			context.Staff.Remove(staff);
			context.SaveChanges();
		}
	}
}