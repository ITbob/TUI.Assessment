using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TUI.Data.Access.Source.Unit;
using TUI.Login.source;
using TUI.Sandbox.Models;

namespace TUI.Sandbox.Controllers
{
    public class LoginController : BasicController
    {
        private readonly IUnit<User> _loginUnit;

        public LoginController(IUnit<User> loginUnit)
        {
            this._loginUnit = loginUnit;
        }

        // GET: Login
        public ActionResult Index()
        {
            UserViewModel viewModel = new UserViewModel
            {
                Authentifie = HttpContext.User.Identity.IsAuthenticated
            };

            using(var session = _loginUnit.GetSession())
            {
                var repo = session.GetRepository();

                int id = 0;
                if (int.TryParse(HttpContext.User.Identity.Name, out id)
                    && HttpContext.User.Identity.IsAuthenticated)
                {
                    viewModel.User = repo.Find(u => u.Id == id).Single();
                }

                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using(var session = this._loginUnit.GetSession())
                {
                    var repo = session.GetRepository();

                    var result = repo.Find(u => 
                        u.Name == viewModel.User.Name 
                        && u.Password == viewModel.User.Password);

                    if(result.Count() == 1)
                    {
                        User user = result.Single();
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                                return Redirect(returnUrl);
                            return Redirect("/");
                        }
                    }

                    ModelState.AddModelError("Utilisateur.Prenom", "Prénom et/ou mot de passe incorrect(s)");
                }


            }
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (var session = this._loginUnit.GetSession())
                {
                    var repo = session.GetRepository();
                    repo.Add(user);
                    session.Complete();
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                    return Redirect("/");
                }

            }

            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}