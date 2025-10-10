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
		public IActionResult Index()
		{
			return View();
		}
		private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
		{
			var departmentsQuery = from d in _context.Departments
								   orderby d.Name
								   select d;
			ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			PopulateDepartmentsDropDownList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("CourseId,Title,Credits,DepartmentID")] Models.Course course)
		{
			if (ModelState.IsValid)
			{
				_context.Add(course);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			PopulateDepartmentsDropDownList(course.DepartmentID);
			return View(course);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			ViewData["action"] = "Edit";
			var course = await _context.Courses.FirstOrDefaultAsync(d => d.CourseId == id);
			return View(nameof(Create), course);
		}
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([Bind("CourseId,Title,Credits,Enrollments,Department,DepartmentID,CourseAssignments")] Course course)
		{
			if (ModelState.IsValid)
			{
				_context.Courses.Update(course);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Index));
		}

	}
}
