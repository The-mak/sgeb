using System.Web.Mvc;

namespace SGEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}
