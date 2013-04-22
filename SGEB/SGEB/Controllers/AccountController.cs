using SGEB.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace SGEB.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ViewResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginModelView login)
        {
            if (ModelState.IsValid)
            {
                login.Username = login.Username.ToLower();

                if (Membership.ValidateUser(login.Username, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Username, login.KeepLogged);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Nome de usuário ou senha inválidos");
            }
            
            return View();
        }

        [HttpGet]
        public RedirectToRouteResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
