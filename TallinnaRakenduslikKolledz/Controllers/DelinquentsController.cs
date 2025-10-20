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
	}
}
