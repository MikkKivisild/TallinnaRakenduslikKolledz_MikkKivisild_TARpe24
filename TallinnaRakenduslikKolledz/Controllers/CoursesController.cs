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
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewData["action"] = "Create";
			PopulateDepartmentsDropDownList();
			return View("CreateEdit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("CourseId,Title,Credits,DepartmentID")] Course course)
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
			if (course == null)
			{
				return NotFound();
			}
			return View("CreateEdit", course);
		}
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([Bind("CourseId,Title,Credits,Enrollments,Department,DepartmentID,CourseAssignments")] Course course, int id)
		{
            course.CourseId = id;
            if (id != course.CourseId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
			{
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index),course);
        }
        public async Task<IActionResult> Details(int? id)
		{
			ViewData["action"] = "Details";
			if (id == null)
			{
				return NotFound();
			}
			var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId== id);

			if (course == null)
			{
				return NotFound();
			}
			return View("DetailsDelete", course);
		}
	}
}
