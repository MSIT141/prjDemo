using prjDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjDemo.Controllers
{
    public class AController : Controller
    {
        public string sayHello()
        {
            return "Hello World!!!";
        }

        public ActionResult demoFileUpdate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult demoFileUpdate(HttpPostedFileBase fileBase)
        {
            fileBase.SaveAs(@"C:\Users\hankshsu\Desktop\prjDemo\images\");
            return View();
        }
        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\note\01.jpg");
            Response.End();
            return "";
        }

        static int count = 0;
        public ActionResult showCountbySeeion()
        {
            int count = 0;

            if (Session["KK"] != null)
                count = (int)Session["KK"];

            count++;

            Session["KK"] = count;
            ViewBag.x = count;
            return View();
        }

        public ActionResult showCountbyCookie()
        {
            int count = 0;

            if (Request.Cookies["KK"] != null)
                count = Convert.ToInt32(Request.Cookies["KK"].Value);

            count++;

            HttpCookie cookie = new HttpCookie("KK");
            cookie.Value = count.ToString();
            cookie.Expires = DateTime.Now.AddSeconds(200);
            Response.Cookies.Add(cookie);

            ViewBag.x = count;
            return View();
        }

        public string demoRequest()
        {
            string s = Request.QueryString["productid"];
            if (s == "1")
            {
                return "PS5 加入購物車";
            }
            else if (s == "2")
            {
                return "XBOX 加入購物車";
            }
            else if (s == "3")
            {
                return "SWITCH 加入購物車";
            }
            else
            {
                return "找不到該產品資料";
            }
        }

        public string demoParameter(int? id)
        //代表允許數值型別為空值
        {
            if (id == 1)
            {
                return "PS5 加入購物車";
            }
            else if (id == 2)
            {
                return "XBOX 加入購物車";
            }
            else if (id == 3)
            {
                return "SWITCH 加入購物車";
            }
            else
            {
                return "找不到該產品資料";
            }
        }

        public string queryId(int? id)
        {
            if (id == null)
                return "Not Found";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            string s = "Not Found Data";

            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tCustomer where tid =" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    s = reader["tName"].ToString() + " / " + reader["tPhone"].ToString();
            }

            return s;


        }

        public string lotto()
        {
            return (new CLottoGen()).getLotto();
        }


        // GET: A
        public ActionResult showByid(int? id)
        {
            CCustomer c = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";

                using (con)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from tCustomer where tid =" + id.ToString(), con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        c = new CCustomer();
                        c.id = (int)reader["tid"];
                        c.name = reader["tName"].ToString();
                        c.phone = reader["tPhone"].ToString();
                        c.address = reader["tAddress"].ToString();
                        ViewBag.KK = c;
                    }

                }
            }

            return View();
        }

        public string test()
        {
            CCustomer c = new CCustomer()
            {
                name = "Amy",
                phone = "099999999",
                address = "XXXXXXXXXXX",
                password = "456777"
            };

            (new CCustomerFactory()).insert(c);
            return "成功";
        }

        public string test2(int? id)
        {
            if (id == null)
                return "Not id";
            (new CCustomerFactory()).delete((int)id);
            return "Delete Success";
        }

        public string test3()
        {
            CCustomer c = new CCustomer()
            {
                id = 3,
                password = "0000000"
            };

            (new CCustomerFactory()).update(c);
            return "成功修改";
        }

        public string testQueryAll(int? id)
        {
            return "客戶數: " + (new CCustomerFactory()).queryAll().Count();
        }

        public ActionResult demoForm(int? id)
        {
            ViewBag.Ans = "?";
            if (!String.IsNullOrEmpty(Request.Form["txtA"]))
            {
                int a = Convert.ToInt32(Request.Form["txtA"]);
                int b = Convert.ToInt32(Request.Form["txtB"]);
                int c = Convert.ToInt32(Request.Form["txtC"]);

                ViewBag.A = Request.Form["txtA"];
                ViewBag.B = Request.Form["txtB"];
                ViewBag.C = Request.Form["txtC"];
                //ViewBag.Ans = a + b;
                double z = Math.Sqrt(b * b - 4 * a * c);
                ViewBag.Ans = ((-b + z) / 2 * a).ToString("0.0#") + "/ ";
                ViewBag.Ans += ((-b - z) / 2 * a).ToString("0.0#");
            }
            return View();
        }


    }
}