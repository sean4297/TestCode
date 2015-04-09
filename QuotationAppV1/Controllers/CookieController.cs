using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Music.Controllers
{
    public class CookieController : Controller
    {
        // GET: Cookie
        public ActionResult Index(string val)
        {
            HttpCookie c = Request.Cookies.Get("mycookie");

            if (c == null)
            {
                ViewBag.State = "Add";
            }
            else
            {
                ViewBag.State = "Expire";
            }

            if (!string.IsNullOrEmpty(val))
            {
                if (val == "Add")
                {
                    c = new HttpCookie("mycookie", "Hello there");
                    c["username"] = "someuser";
                    ViewBag.State = "Persist";
                }
                else if (val == "Persist")
                {
                    c.Expires = DateTime.Now.AddHours(5);
                    ViewBag.State = "Expire";
                }
                else
                {
                    c.Expires = DateTime.Now.AddHours(-5);
                    ViewBag.State = "Add";

                }
                Response.Cookies.Add(c);
            }

            return View();
        }
    }
}