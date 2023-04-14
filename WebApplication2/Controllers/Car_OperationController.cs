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
    public class Car_OperationController : Controller
    {
        private TCarDBEntities1 db = new TCarDBEntities1();

        // GET: Car_Operation
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Index()
        {
            var car_Operation = db.Car_Operation.Include(c => c.Sign);
            return View(car_Operation.ToList());
        }

        // GET: Car_Operation/Details/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Operation car_Operation = db.Car_Operation.Find(id);
            if (car_Operation == null)
            {
                return HttpNotFound();
            }
            return View(car_Operation);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // GET: Car_Operation/Create
        public ActionResult Create()
        {
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID");
            return View();
        }

        // POST: Car_Operation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoID,CID,TimeDate,CarType")] Car_Operation car_Operation)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("email");
                var user = db.Signs.Where(s => s.Email == cookie.Value).ToList();
                car_Operation.CID = user[0].CID;
                db.Car_Operation.Add(car_Operation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", car_Operation.CID);
            return View(car_Operation);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // GET: Car_Operation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Operation car_Operation = db.Car_Operation.Find(id);
            if (car_Operation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", car_Operation.CID);
            return View(car_Operation);
        }

        // POST: Car_Operation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoID,CID,TimeDate,CarType")] Car_Operation car_Operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car_Operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", car_Operation.CID);
            return View(car_Operation);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // GET: Car_Operation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Operation car_Operation = db.Car_Operation.Find(id);
            if (car_Operation == null)
            {
                return HttpNotFound();
            }
            return View(car_Operation);
        }
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        // POST: Car_Operation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car_Operation car_Operation = db.Car_Operation.Find(id);
            db.Car_Operation.Remove(car_Operation);
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
