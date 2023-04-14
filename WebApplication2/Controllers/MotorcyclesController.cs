using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Attribute;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MotorcyclesController : Controller
    {
        private TCarDBEntities1 db = new TCarDBEntities1();

        // GET: Motorcycles
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Index()
        {
            var motorcycles = db.Motorcycles.Include(m => m.Sign);
            return View(motorcycles.ToList());
        }

        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // GET: Motorcycles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorcycle motorcycle = db.Motorcycles.Find(id);
            if (motorcycle == null)
            {
                return HttpNotFound();
            }
            return View(motorcycle);
        }

        // GET: Motorcycles/Create
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Create()
        {
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID");
            return View();
        }

        // POST: Motorcycles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Create([Bind(Include = "MotorcycleID,CID,MotorcycleType,MotorcycleLiceinse,Model,CaseNo")] Motorcycle motorcycle)
        {
            if (ModelState.IsValid)
            {
                db.Motorcycles.Add(motorcycle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", motorcycle.CID);
            return View(motorcycle);
        }

        // GET: Motorcycles/Edit/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorcycle motorcycle = db.Motorcycles.Find(id);
            if (motorcycle == null)
            {
                return HttpNotFound();
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", motorcycle.CID);
            return View(motorcycle);
        }

        // POST: Motorcycles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Edit([Bind(Include = "MotorcycleID,CID,MotorcycleType,MotorcycleLiceinse,Model,CaseNo")] Motorcycle motorcycle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(motorcycle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", motorcycle.CID);
            return View(motorcycle);
        }

        // GET: Motorcycles/Delete/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorcycle motorcycle = db.Motorcycles.Find(id);
            if (motorcycle == null)
            {
                return HttpNotFound();
            }
            return View(motorcycle);
        }

        // POST: Motorcycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult DeleteConfirmed(int id)
        {
            Motorcycle motorcycle = db.Motorcycles.Find(id);
            db.Motorcycles.Remove(motorcycle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
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
