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
using F2018Pranks.Models;

namespace F2018Pranks.Controllers.Api
{
    public class PranksAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PranksAPI
        public IQueryable<Prank> GetPranks()
        {
            return db.Pranks;
        }

        // GET: api/PranksAPI/5
        [ResponseType(typeof(Prank))]
        public IHttpActionResult GetPrank(int id)
        {
            Prank prank = db.Pranks.Find(id);
            if (prank == null)
            {
                return NotFound();
            }

            return Ok(prank);
        }

       

        // POST: api/PranksAPI
        [ResponseType(typeof(Prank))]
        public IHttpActionResult PostPrank(Prank prank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pranks.Add(prank);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = prank.PrankId }, prank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrankExists(int id)
        {
            return db.Pranks.Count(e => e.PrankId == id) > 0;
        }
    }
}