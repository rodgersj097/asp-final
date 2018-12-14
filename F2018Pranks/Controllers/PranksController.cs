using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using F2018Pranks.Models;

namespace F2018Pranks.Controllers
{  [Authorize]
    public class PranksController : Controller
    {
       // private DbModel db = new DbModel();
        private IMockPrank db;

        public PranksController()
        {
            this.db = new EFPrank();
        }

        public PranksController(IMockPrank db)
        {
            this.db = db;
        }
        
        // GET: Pranks
        public ActionResult Index()
        {
            return View("Index", db.Pranks.ToList());
        }
        
        // GET: Pranks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            // Prank prank = db.Pranks.Find(id);
            Prank prank = db.Pranks.SingleOrDefault(p => p.PrankId == id); 
            if (prank == null)
            {
                // return HttpNotFound();
                return View("Error"); 
            }
            return View(prank);
        }

        // GET: Pranks/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Pranks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrankId,Name,Description,Photo")] Prank prank)
        {
            if (ModelState.IsValid)
            {
                // db.Pranks.Add(prank);
                // db.SaveChanges();
                db.Save(prank);
                return RedirectToAction("Index");
            }

            return View(prank);
        }
    }
}
