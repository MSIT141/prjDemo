using prjDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjDemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            CCustomer user = Session[CDictionary.SK_LOGIN_USER] as CCustomer;
            if (user == null)
                return RedirectToAction("Login");
            return View(user);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vm)
        {
            CCustomer cust = (new CCustomerFactory()).queryByPhone(vm.txtAccount);
            if (cust.password.Equals(vm.txtPassword))
            {
                Session[CDictionary.SK_LOGIN_USER] = cust;
                return RedirectToAction("Home");
            }
                
            return View();
        }
    }
}