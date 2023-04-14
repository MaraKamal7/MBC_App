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
    public class BikesController : Controller
    {
        private TCarDBEntities1 db = new TCarDBEntities1();

        // GET: Bikes
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Index()
        {
            var bikes = db.Bikes.Include(b => b.Sign);
            return View(bikes.ToList());
        }

        // GET: Bikes/Details/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // GET: Bikes/Create
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Create()
        {
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID");
            return View();
        }

        // POST: Bikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Create([Bind(Include = "BikeID,CID,BikeType,BikeModel,BikeRecipet")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                db.Bikes.Add(bike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", bike.CID);
            return View(bike);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // GET: Bikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", bike.CID);
            return View(bike);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BikeID,CID,BikeType,BikeModel,BikeRecipet")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", bike.CID);
            return View(bike);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // GET: Bikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bike bike = db.Bikes.Find(id);
            db.Bikes.Remove(bike);
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
