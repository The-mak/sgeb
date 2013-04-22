using SGEB.Model;
using System.Web.Mvc;

namespace SGEB.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private ConfigurationRepository repository;

        public ConfigurationController()
        {
            this.repository = new ConfigurationRepository();
        }

        [HttpGet]
        public ViewResult Configurations()
        {
            return View("Configurations", this.repository.Configuration);
        }

        [HttpPost]
        public ActionResult Configurations(Configuration config)
        {
            if (ModelState.IsValid)
            {
                this.repository.Configuration = config;
                return RedirectToAction("Index", "Home");
            }
            else
                TempData["ErrorMessage"] = "Ocorreu um erro ao salvar os dados";

            return View("Configurations", config);
        }
    }
}
