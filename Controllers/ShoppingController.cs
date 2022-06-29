using prjDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjDemo.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            var qry = from q in (new dbDemoEntities()).tProduct
                      select q;

            return View(qry);
        }

        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart = Session[CDictionary.SK_PURCHASE_PRODUCT_ITEM] as List<CShoppingCartItem>;
            if (cart == null)
                return RedirectToAction("List");
            return View(cart);
        }

        public ActionResult AddToSession(int? id)
        {
            var qry = (new dbDemoEntities()).tProduct.FirstOrDefault(X => X.fId == id);
            if (qry == null)
                return RedirectToAction("List");
            return View(qry);
        }
        [HttpPost]
        public ActionResult AddToSession(CAddShoppingViewModel vm)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct q = db.tProduct.FirstOrDefault(x => x.fId == vm.txtid );
            if (q == null)
                return RedirectToAction("List");

            List<CShoppingCartItem> list = Session[CDictionary.SK_PURCHASE_PRODUCT_ITEM] as List<CShoppingCartItem>;
            if (list == null)
            {
                list = new List<CShoppingCartItem>();
                Session[CDictionary.SK_PURCHASE_PRODUCT_ITEM] = list;
            }

            CShoppingCartItem item = new CShoppingCartItem()
            {
                id = vm.txtid,
                count = vm.txtCount,
                price = (decimal)q.fPrice,
                product = q
            };

            list.Add(item);

            return RedirectToAction("List");
        }
    }
}