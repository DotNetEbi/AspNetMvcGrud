using AspNetMvcGrud.Domain.Filter;
using AspNetMvcGrud.Interface;
using AspNetMvcGrud.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcGrud.Web.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmployeeService _employeeService;

		public EmployeeController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		[HttpPost("Filter")]
		[Route("Employee/Index/Filter")]
		public async Task<IActionResult> Index(EmployeeFilter filter)
		{
			var employeeList = await _employeeService.GetListByAsync(filter);

			ControllerHelper.EmployeeList = employeeList;

			return View(employeeList);
		}

		public IActionResult SortIndex(string sortOrder)
		{
			var employeeList = ControllerHelper.EmployeeList;

			ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["ContactNumberSortParm"] = sortOrder == "ContactNumber" ? "ContactNumber_desc" : "ContactNumber";
			ViewData["DOBSortParm"] = sortOrder == "DataOfBirth" ? "DataOfBirth_desc" : "DataOfBirth";
			ViewData["DOJSortParm"] = sortOrder == "JoinedDate" ? "JoinedDate_desc" : "JoinedDate";
            ViewData["GenderSortParm"] = sortOrder == "Gender" ? "Gender" : "";

            switch (sortOrder)
			{
				case "name_desc":
					employeeList = employeeList.OrderByDescending(s => s.Name);
					break;
				case "ContactNumber":
					employeeList = employeeList.OrderBy(s => s.ContactNumber);
					break;
				case "DataOfBirth_desc":
					employeeList = employeeList.OrderByDescending(s => s.DateOfBirth);
					break;
				case "DataOfBirth":
					employeeList = employeeList.OrderBy(s => s.DateOfBirth);
					break;
				case "JoinedDate_desc":
					employeeList = employeeList.OrderByDescending(s => s.JoinedDate);
					break;
				case "JoinedDate":
					employeeList = employeeList.OrderBy(s => s.JoinedDate);
					break;
                case "Gender":
                    employeeList = employeeList.OrderBy(s => s.Gender);
                    break;
                default:
					employeeList = employeeList.OrderBy(s => s.Name);
					break;
			}


			return View("Index", employeeList);
		}
	}
}
