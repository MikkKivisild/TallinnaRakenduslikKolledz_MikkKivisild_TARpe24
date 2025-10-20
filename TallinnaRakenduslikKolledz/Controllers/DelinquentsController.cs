using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolledz.Controllers
{
	public class DelinquentsController : Controller
	{
		private readonly SchoolContext _context;
		public DelinquentsController(SchoolContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _context.Delinquents.ToListAsync());
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id, FirstName, LastName, Violation, Violator, Description")] Delinquent delinquent)
		{
			if (ModelState.IsValid)
			{
				_context.Add(delinquent);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(delinquent);
		}
        public async Task<IActionResult> Details(int? id)
		{
            if (id == null)
			{
                return NotFound();
            }
            var delinquent = await _context.Delinquents.FirstOrDefaultAsync(x => x.Id == id);
            if (delinquent == null)
			{
                return NotFound();
            }
            return View(delinquent);
        }
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var delinquent = await _context.Delinquents.FindAsync(id);
			if (delinquent == null)
			{
				return NotFound();
			}
			return View(delinquent);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, Violation, Violator, Description")] Delinquent delinquent)
		{
			delinquent.Id = id;
			if (id != delinquent.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				_context.Update(delinquent);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
