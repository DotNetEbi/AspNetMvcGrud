using AspNetMvcGrud.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetMvcGrud.Web.Helper
{
	public class ControllerHelper
	{
		public static IEnumerable<Employee> EmployeeList { get; set; } = new List<Employee>();
		public static List<SelectListItem> GenderList { get; set; }
        public  static string SelectedGender { get; set; }
    }
}
