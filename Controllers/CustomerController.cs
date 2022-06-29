using prjDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            string keyword = Request.Form["txtKeyword"];
            CCustomerFactory f = new CCustomerFactory();
            List<CCustomer> list = null;
            if (String.IsNullOrEmpty(keyword))
                list = f.queryAll();
            else
                list = f.queryByKeyword(keyword);
            return View(list);
        }
        // GET: Customer
        public ActionResult New()
        {
            return View();
        }
        // GET: Customer
        public ActionResult Save()
        {
            CCustomer c = new CCustomer();
            c.name = Request.Form["txtName"];
            c.phone = Request.Form["txtPhone"];
            c.address = Request.Form["txtAddress"];
            c.password = Request.Form["txtPassword"];

            (new CCustomerFactory()).insert(c);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            (new CCustomerFactory()).delete((int)id);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            CCustomer c = (new CCustomerFactory()).queryById((int)id);
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(CCustomer c)
        {
            (new CCustomerFactory()).update(c);
            return RedirectToAction("Index");
        }
    }
}