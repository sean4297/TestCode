using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuotationAppV1.Models;


namespace QuotationAppV1.Controllers
{
    //[RoutePrefix("api/Cool")]
    public class CoolController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Cool
        public IQueryable<Quotation> GetQuotations()
        {
            return db.Quotations;
        }


        //[Route("RandomQuote")]
        //[ResponseType(typeof(Quotation))]
        //public IHttpActionResult RandomQuote()
        //{
        //    Quotation quotation = db.Quotations.Find(44);
        //    if (quotation == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(quotation);
        //}


        [Route("GetDayQuote")]
        [HttpGet]
        public IHttpActionResult GetDayQuote()
        {
            var number = db.Quotations.Count();

            Random rnd = new Random();
            int day = rnd.Next(number);
            IHttpActionResult daily = GetQuotation(day);
            if (daily == null)
            {
                day = rnd.Next(number);
                daily = GetQuotation(day);
            }
            return daily;
        }



        // GET: api/Cool/5
        //[Route("GetDayQuote")]
        //[Route(Name ="")]
        [ResponseType(typeof(Quotation))]
        public IHttpActionResult GetQuotation(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return NotFound();
            }

            return Ok(quotation);
        }

        // PUT: api/Cool/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuotation(int id, Quotation quotation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quotation.QuotationID)
            {
                return BadRequest();
            }

            db.Entry(quotation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuotationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cool
        [ResponseType(typeof(Quotation))]
        public IHttpActionResult PostQuotation(Quotation quotation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quotations.Add(quotation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = quotation.QuotationID }, quotation);
        }

        // DELETE: api/Cool/5
        [ResponseType(typeof(Quotation))]
        public IHttpActionResult DeleteQuotation(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return NotFound();
            }

            db.Quotations.Remove(quotation);
            db.SaveChanges();

            return Ok(quotation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuotationExists(int id)
        {
            return db.Quotations.Count(e => e.QuotationID == id) > 0;
        }


        [Route("randomQuote")]
        [ResponseType(typeof(ViewModel))]
        [HttpGet]
        public IHttpActionResult randomQuote()
        {

            Random randomNumber = new Random();
            //int TotalRows = db.Quotations.Count();

            var q = db.Quotations.ToArray();
            int i = q.Count();
            Quotation thisQuote = q[randomNumber.Next(1, i)];

            //while (thisQuote == null)
            //{
            //    thisQuote = db.Quotations.Find(randomNumber.Next(1, TotalRows));
            //}

            ViewModel UseQuote = new ViewModel();
            UseQuote.Author = thisQuote.Author;
            UseQuote.Category = thisQuote.Category.Name;
            UseQuote.Quote = thisQuote.Quote;



            return Ok(UseQuote);

            //Quotation thisQuote = db.Quotations.Find(33);

            //return Ok(thisQuote);
        }


    }
}

        