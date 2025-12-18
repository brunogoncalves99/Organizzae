using Microsoft.AspNetCore.Mvc;

namespace Organizzae.Web.Controllers
{
    public class ReceitasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
