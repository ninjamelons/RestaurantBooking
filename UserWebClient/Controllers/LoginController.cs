using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebClient.Models;
using UserWebClient.Logic;
using System.Threading.Tasks;

namespace UserWebClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly CustomerService.ICustomerService _proxy;

        public LoginController(CustomerService.ICustomerService proxy)
        {
            this._proxy = proxy;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Login/Register
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                bool success = await _proxy.RegisterUserAsync(model.Customer, AuthenticationUtil.HashPasswordString(model.Customer.Email, model.Password));
                if (!success)
                    return View();

                Session["roleId"] = model.Customer.RoleId;
                Session["customerId"] = model.Customer.Id;
                Session["email"] = model.Customer.Email;
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            { 
                var customer = await _proxy.LoginCustomerAsync(model.Email, AuthenticationUtil.HashPasswordString(model.Email, model.Password));
                if (customer == null)
                    return RedirectToAction("Login");

                Session["roleId"] = customer.RoleId;
                Session["customerId"] = customer.Id;
                Session["email"] = customer.Email;
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            Session["roleId"] = null;
            Session["customerId"] = null;
            Session["email"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
