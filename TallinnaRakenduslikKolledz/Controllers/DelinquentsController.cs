using Microsoft.AspNetCore.Mvc;

namespace TallinnaRakenduslikKolledz.Controllers
{
    public class DelinquentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
