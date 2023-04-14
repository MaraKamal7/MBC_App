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
    public class Bike_OperationController : Controller
    {

        private TCarDBEntities1 db = new TCarDBEntities1();

        // GET: Bike_Operation
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Index()
        {
            var bike_Operation = db.Bike_Operation.Include(b => b.Sign);
            return View(bike_Operation.ToList());
        }

        // GET: Bike_Operation/Details/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike_Operation bike_Operation = db.Bike_Operation.Find(id);
            if (bike_Operation == null)
            {
                return HttpNotFound();
            }
            return View(bike_Operation);
        }

        // GET: Bike_Operation/Create
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Create()
        {
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID");
            return View();
        }

        // POST: Bike_Operation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoID,CID,TimeDate,BikeType")] Bike_Operation bike_Operation)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("email");
                var user = db.Signs.Where(s => s.Email == cookie.Value).ToList();
                bike_Operation.CID = user[0].CID;
                db.Bike_Operation.Add(bike_Operation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", bike_Operation.CID);
            return View(bike_Operation);
        }

        // GET: Bike_Operation/Edit/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike_Operation bike_Operation = db.Bike_Operation.Find(id);
            if (bike_Operation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", bike_Operation.CID);
            return View(bike_Operation);
        }

        // POST: Bike_Operation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoID,CID,TimeDate,BikeType")] Bike_Operation bike_Operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bike_Operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", bike_Operation.CID);
            return View(bike_Operation);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // GET: Bike_Operation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike_Operation bike_Operation = db.Bike_Operation.Find(id);
            if (bike_Operation == null)
            {
                return HttpNotFound();
            }
            return View(bike_Operation);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // POST: Bike_Operation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bike_Operation bike_Operation = db.Bike_Operation.Find(id);
            db.Bike_Operation.Remove(bike_Operation);
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
