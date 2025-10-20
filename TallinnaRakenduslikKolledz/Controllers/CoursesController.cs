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
		public async Task<IActionResult> Details(int? id)
		{
			ViewData["action"] = "Details";
			if (id == null)
			{
				return NotFound();
			}
			var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);

			if (course == null)
			{
				return NotFound();
			}
			return View("DetailsDelete", course);
		}
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			ViewData["action"] = "Delete";
			if (id == null)
			{
				return NotFound();
			}
			var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
			if (course == null)
			{
				return NotFound();
			}
			return View("DetailsDelete", course);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			ViewData["action"] = "Delete";
			var course = await _context.Courses.FindAsync(id);
			_context.Courses.Remove(course);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index), course);
		}
	}
}
