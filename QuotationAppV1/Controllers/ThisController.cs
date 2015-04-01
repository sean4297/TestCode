using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuotationAppV1.Models;

namespace QuotationAppV1.Controllers
{
    public class ThisController : Controller
    {

        public static bool switch1 = false;

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: This
        //public ActionResult Index(string searchString)
        //{
        //    var quotes = from m in db.Quotations
        //                 select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        quotes = quotes.Where(s => s.Quote.Contains(searchString)
        //                                || s.Author.Contains(searchString)
        //                                || s.Category.Name.Contains(searchString));
        //    }

        //    return View(quotes);
        //}

        //public ActionResult Index()
        //{
        //    var quotations = db.Quotations.Include(q => q.Category);
        //    return View(quotations.ToList());
        //}

        // GET: Quotations
        public ActionResult Index(string searchString)
        {
            ViewBag.switch1 = false;

           
            var quotations = db.Quotations.Include(q => q.Category);

            var quote = from q in db.Quotations
                        select q;
            if (!String.IsNullOrEmpty(searchString))
            {
                quote = quote.Where(s => s.Author.Contains(searchString) || s.Category.Name.Contains(searchString) || s.Quote.Contains(searchString));
                ViewBag.switch1 = true;
            }

            return View(quote.ToList());
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
        public ActionResult Create([Bind(Include = "QuotationID,CategoryID,Quote,Author,DateAdded")] Quotation quotation, String Name)
        {
            Category newCategory = new Category();
            //s.Category.Name.Contains(searchString)
            if (!String.IsNullOrEmpty(Name))
            {
                int intId;
                int[] categoryArray = db.Categories.Where(c => c.Name == Name).Select(c => c.CategoryID).ToArray();
                if (categoryArray.Count() > 0)
                {
                    intId = categoryArray[0];
                }
                else
                {
                    newCategory.Name = Name;
                    db.Categories.Add(newCategory);
                    intId = newCategory.CategoryID;
                }

                newCategory = db.Categories.Find(intId);
                quotation.CategoryID = newCategory.CategoryID;
            }

            if (ModelState.IsValid)
            {

                quotation.DateAdded = DateTime.Now;
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
