using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolledz.Controllers
{
	public class InstructorsController : Controller
	{
		private readonly SchoolContext _context;
		public InstructorsController(SchoolContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(int? id, int? coureId)
		{
			var vm = new InstructorIndexData();
			vm.Instructors = await _context.Instructors
				.Include(i => i.OfficeAssignment)
				.Include(i => i.CourseAssignments)
				.ToListAsync();
			return View(vm);
		}
	}
}
