using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            //dbDemoEntities db = new dbDemoEntities();
            //IEnumerable < tProduct > p= null;

            var qry = from q in (new dbDemoEntities()).tProduct
                      select q;

            if (!String.IsNullOrEmpty(keyword))
                qry = qry.Where(x => x.fName.Contains(keyword));


            return View(qry);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities();
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct p = db.tProduct.FirstOrDefault(t => t.fId == id);
            if (p != null)
            {
                db.tProduct.Remove(p);
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct p = db.tProduct.FirstOrDefault(t => t.fId == id);
            if (p == null)
                return RedirectToAction("List");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(tProduct p)
        {
            if (string.IsNullOrEmpty(p.fName))
            {

            }
            else
            {
                dbDemoEntities db = new dbDemoEntities();
                tProduct qry = db.tProduct.FirstOrDefault(t => t.fId == p.fId);

                if (p.photo != null)
                {
                    string pname = Guid.NewGuid().ToString() + p.fName +".jpg";
                    //p.photo.SaveAs(Server.MapPath("../../images/pname"));
                    //qry.fImagePath = pname;
                    qry.image = ConvertToBytes(p.photo);
                }

                if (qry != null)
                {
                    qry.fName = p.fName;
                    qry.fCost = p.fCost;
                    qry.fQty = p.fQty;
                    qry.fPrice = p.fPrice;
                }
                db.SaveChanges();
                
            }
            return RedirectToAction("List");
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public byte[] GetImageFromDataBase(int Id)
        {
            dbDemoEntities db = new dbDemoEntities();
            var q = from temp in db.tProduct where temp.fId == Id select temp.image;
            byte[] cover = q.First();
            return cover;
        }
    }
}