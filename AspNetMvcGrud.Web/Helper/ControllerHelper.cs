using AspNetMvcGrud.Domain;

namespace AspNetMvcGrud.Web.Helper
{
	public class ControllerHelper
	{
		public static IEnumerable<Employee> EmployeeList { get; set; } = new List<Employee>();

	}
}
