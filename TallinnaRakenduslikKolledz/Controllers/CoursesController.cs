using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolledz.Controllers
{
	public class CoursesController : Controller
	{
		private readonly SchoolContext _context;
		public CoursesController(SchoolContext context)
		{ 
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var vm = _context.Courses;
			return View(await vm.ToListAsync());
		}
		private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
		{
			var departmentsQuery = from d in _context.Departments
								   orderby d.Name
								   select d;
			ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
		}
	}
}
