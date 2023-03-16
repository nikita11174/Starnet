using Flenov.BL.Auth;
using Flenov.ViewMapper;
using Flenov.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Flenov.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthBL authBl;

        public LoginController(IAuthBL authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id = await authBl.Authinticate(model.Email!, model.Password!, model.RememberMe == true);
                if (id > 0)
                    return Redirect("/");
            }

            return View("Index", model);
        }
    }
}