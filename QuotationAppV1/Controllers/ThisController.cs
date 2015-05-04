using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuotationAppV1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Security;

namespace QuotationAppV1.Controllers
{
    public class ThisController : Controller
    {

        //: Switch1 shows the Clear Button
        //: Switch2 shows the Add Quote / My Quote Button
        //: Switch3 shows the Hide button

        public static bool switch1 = false;
        public static bool switch2 = false;
        public static bool switch3 = false;
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;
        private static ViewModel randQuote;


        public ThisController(){
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Quotations
        public ActionResult Index(string searchString, int? id)
        {
            ViewBag.switch1 = false;
            ViewBag.switch2 = false;
            ViewBag.switch3 = false;

            //ViewBag.Quote = "t";


            //var baseUri = new Uri("http://localhost:53365/");
            var baseUri = new Uri("http://quotationapp-seanoneill.azurewebsites.net/");
            HttpClient client = new HttpClient();
            client.BaseAddress = baseUri;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("randomQuote").Result;

            if (response.IsSuccessStatusCode)
            {

                HttpCookie e = Request.Cookies.Get("newcookie");

                if (e == null)
                {
                    e = new HttpCookie("newcookie");
                    randQuote = response.Content.ReadAsAsync<ViewModel>().Result;
                    e.Value = randQuote.Quote + " - " + randQuote.Author;
                    ViewBag.Quote = randQuote.Quote + " - " + randQuote.Author;
                    Response.Cookies.Add(e);
                }
                else if (e != null)
                {
                    ViewBag.Quote = e.Value;
                }


                
                
                
                
                
            }



            






            if (manager.FindById(User.Identity.GetUserId()) != null)
            {
                ViewBag.switch2 = true;
            }

            var quotations = db.Quotations.Include(q => q.Category);

            var quote = from q in db.Quotations
                        select q;

            var user = manager.FindById(User.Identity.GetUserId());

            //quote = quote.Where(s => s.ApplicationUser.ToString() == userid);

            if (!String.IsNullOrEmpty(searchString))
            {
                quote = quote.Where(s => s.Author.Contains(searchString) || s.Category.Name.Contains(searchString) || s.Quote.Contains(searchString));        
                ViewBag.switch1 = true;
            }
            //Cookie 

            //HttpCookie c = new HttpCookie("c");

            //// Set the cookie value.
            //c.Value = quotation.QuotationID.ToString();
            //// Set the cookie expiration date.
            //// Add the cookie.
            //Response.Cookies.Add(c);

            HttpCookie c = Request.Cookies.Get("mycookie");
            //Quotation quotation = db.Quotations.Find(id);

            if (c != null)
            {
                ViewBag.switch3 = true;
                string val = c.Value;
                var filterList = val.Split(',').Select(n => int.Parse(n));

                var check = filterList.ToList();

                //quote = quote.Where(s => s.QuotationID != filterList.);
                quote = quote.Where(n => !filterList.Select(n1 => n1).Contains(n.QuotationID));
            }

            return View(quote.ToList());
        }


        public ActionResult MyQuotes()
        {
            ViewBag.switch1 = false;
            ViewBag.switch2 = false;
            ViewBag.switch3 = false;
            ViewBag.switch4 = false;

            if (manager.FindById(User.Identity.GetUserId()) != null)
            {
                ViewBag.switch2 = true;
                
            }

            var quotations = db.Quotations.Include(q => q.Category);
            var quote = from q in db.Quotations
                        select q;

            //var user = manager.FindById(User.Identity.GetUserId());
            var user = User.Identity.GetUserId();

            quote = quote.Where(s => s.UserID == user);


            if (quote != null)
            {
                ViewBag.switch4 = true;
                return View(quote.ToList());
            }
            return View();
        }

        public ActionResult HideQuotes(int? id)
        {
            ViewBag.switch1 = false;
            ViewBag.switch2 = false;
            ViewBag.switch3 = false;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }



            HttpCookie c = Request.Cookies.Get("mycookie");

            if (c == null)
            {
                c = new HttpCookie("mycookie");
                c.Value= quotation.QuotationID.ToString();
                c.Expires = DateTime.Now.AddHours(5);
                
            }
            else if (c != null)
            {
                c.Value = c.Value+","+quotation.QuotationID.ToString();
            }
            ViewBag.switch3 = true;

            string val = c.Value;
            var filterList = val.Split(',').Select(n => int.Parse(n));


            var quotations = db.Quotations.Include(q => q.Category);
            var quote = from q in db.Quotations
                        select q;

            ////var user = manager.FindById(User.Identity.GetUserId());
            //var user = User.Identity.GetUserId();
            var check = filterList.ToList();

            //quote = quote.Where(s => s.QuotationID != filterList.);
            quote = quote.Where(n => !filterList.Select(n1 => n1).Contains(n.QuotationID));

            Response.Cookies.Add(c);
            //return View(quote.ToList());

            return RedirectToAction("Index");
        }

        public ActionResult Unhide(){
            HttpCookie c = Request.Cookies.Get("mycookie");
            c.Value = "";
            c.Expires = DateTime.Now.AddHours(-5);
            Response.Cookies.Add(c);

            return RedirectToAction("Index");

        }

        // GET: This/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }


        // GET: This/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Quotations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "QuotationID,CategoryID,Quote,Author,DateAdded, UserID")] Quotation quotation, String Name)
        {
            Category c1 = new Category();
            //s.Category.Name.Contains(searchString)
            if (!String.IsNullOrEmpty(Name))
            {

                int ID1;
                int[] arr = db.Categories.Where(s => s.Name == Name).Select(s => s.CategoryID).ToArray();

                if (arr.Count() > 0)
                {
                    ID1 = arr[0];
                }
                else
                {
                    c1.Name = Name;
                    db.Categories.Add(c1);
                    ID1 = c1.CategoryID;
                }

                c1 = db.Categories.Find(ID1);
                quotation.CategoryID = c1.CategoryID;
            }

            if (ModelState.IsValid)
            {
                //var user = manager.FindById(User.Identity.GetUserId());
                quotation.UserID = User.Identity.GetUserId(); 
                quotation.DateAdded = DateTime.Now;
                quotation.userEmail = User.Identity.GetUserName();





                //Report Monologue

                //var quotations = db.Quotations.Include(q => q.Category);
                //var quote = from q in db.Quotations
                //            select q;
                

                //var hello = db.Categories;
                //var hello2 = from q2 in db.Categories
                //             select q2;

                

                ////var user = manager.FindById(User.Identity.GetUserId());
                //var user = User.Identity.GetUserId();

                //quote = quote.Where(s => s.UserID == user);

                //var quotes = quote.ToArray();
                //var userCategories = db.Categories.ToArray();

                //for (int i = 0; i < userCategories.Length; i++){
                //    quotation.v.stuff.Add(userCategories[i]);
                //}

                



                db.Quotations.Add(quotation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }

        // POST: This/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "QuotationID,Quote,Author,CategoryID,DateAdded")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                quotation.DateAdded = DateTime.Now;
                db.Entry(quotation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }

        // GET: This/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // POST: This/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            db.Quotations.Remove(quotation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
