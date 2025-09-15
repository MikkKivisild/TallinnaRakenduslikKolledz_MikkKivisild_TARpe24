using Microsoft.AspNetCore.Mvc;

namespace TallinnaRakenduslikKolledz.Controllers
{
	public class InstructorsController : Controller
	{

		public IActionResult Index()
		{
			return View();

		}
	}
}
