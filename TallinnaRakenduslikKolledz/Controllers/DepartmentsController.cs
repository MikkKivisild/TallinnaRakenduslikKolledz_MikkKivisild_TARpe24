using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolledz.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;
        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Departments.Include(d => d.Administrator);
            return View(await schoolContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["action"] = "Details";
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentID == id);

            if (department == null)
            {
                return NotFound();
            }
            return View("DetailsDelete", department);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["action"] = "Delete";
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            return View("DetailsDelete", department);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["action"] = "Delete";
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
