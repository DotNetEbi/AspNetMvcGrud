using AspNetMvcGrud.Domain;
using AspNetMvcGrud.Domain.Filter;
using AspNetMvcGrud.Interface;
using AspNetMvcGrud.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AspNetMvcGrud.Web.Controllers
{
	public class EmployeeController : Controller
	{

        private readonly IEmployeeService _employeeService;

		public EmployeeController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		private IEnumerable<SelectListItem> GetSelectListItems()
		{
			var selectList = new List<SelectListItem>();

			// Get all values of the Gender enum
			var enumValues = Enum.GetValues(typeof(Gender)) as Gender[];
			if (enumValues == null)
				return null;

			foreach (var enumValue in enumValues)
			{
				// Create a new SelectListItem element and set its 
				// Value and Text to the enum value and description.
				selectList.Add(new SelectListItem
				{
					Value = enumValue.ToString(),
					// GetGenderName just returns the Display.Name value
					// of the enum - check out the next chapter for the code of this function.
					Text = GetEnumDisplayName(enumValue)
				});
			}

			return selectList;
		}

		private string GetEnumDisplayName<T>(T value) where T : struct
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

			if (attributes != null && attributes.Any())
			{
				return attributes.First().Description;
			}

			return value.ToString();
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

		//GET
		public IActionResult Create()
		{
			if (ControllerHelper.GenderList?.Count() == default)
				ControllerHelper.GenderList = GetSelectListItems().ToList();

			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Employee obj)
		{
			if (ControllerHelper.GenderList?.Count() == default)
				ControllerHelper.GenderList = GetSelectListItems().ToList();

			obj.Status = CurrentStatus.Active;

			//if (obj.Name == obj.Code.ToString())
			//{
			//    ModelEmployee.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			//}
			if (ModelState.IsValid)
			{
				await _employeeService.SaveAsync(obj);
				TempData["success"] = "Employee created successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		//GET
		public async Task<IActionResult> Edit(int? id)
		{
			if (ControllerHelper.GenderList?.Count() == default)
				ControllerHelper.GenderList = GetSelectListItems().ToList();

			if (id == null || id == 0)
			{
				return NotFound();
			}
			var employeeFromDbFirst = await _employeeService.FindByIDAsync(id ?? 0);

			if (employeeFromDbFirst == null)
			{
				return NotFound();
			}

			return View(employeeFromDbFirst);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Employee obj)
		{
			if (ControllerHelper.GenderList?.Count() == default)
				ControllerHelper.GenderList = GetSelectListItems().ToList();

			if (ModelState.IsValid)
			{
				obj.Status = obj.InActive ? CurrentStatus.Inactive : CurrentStatus.Active;

				await _employeeService.UpdateAsync(obj);
				TempData["success"] = "Employee updated successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			await _employeeService.DeleteAsync(id);

			return RedirectToAction("Index");
		}

		//POST
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public IActionResult DeletePOST(int? id)
		//{
		//    //var obj = _unitOfWork.Employee.FindByIDAsync(id ?? 0);
		//    //if (obj == null)
		//    //{
		//    //    return NotFound();
		//    //}

		//    //_unitOfWork.Employee.(obj);
		//    //_unitOfWork.Save();
		//    //TempData["success"] = "Employee deleted successfully";
		//    //return RedirectToAction("Index");

		//}
	}
}
