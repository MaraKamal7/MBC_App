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
    public class Motorcycle_OperationController : Controller
    {
        private TCarDBEntities1 db = new TCarDBEntities1();

        // GET: Motorcycle_Operation
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Index()
        {
            var motorcycle_Operation = db.Motorcycle_Operation.Include(m => m.Sign);
            return View(motorcycle_Operation.ToList());
        }

        // GET: Motorcycle_Operation/Details/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorcycle_Operation motorcycle_Operation = db.Motorcycle_Operation.Find(id);
            if (motorcycle_Operation == null)
            {
                return HttpNotFound();
            }
            return View(motorcycle_Operation);
        }

        // GET: Motorcycle_Operation/Create
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Create()
        {
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID");
            return View();
        }

        // POST: Motorcycle_Operation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Create([Bind(Include = "CoID,CID,TimeDate,MotorcycleType")] Motorcycle_Operation motorcycle_Operation)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("email");
                var user = db.Signs.Where(s => s.Email == cookie.Value).ToList();
                motorcycle_Operation.CID = user[0].CID;
                db.Motorcycle_Operation.Add(motorcycle_Operation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", motorcycle_Operation.CID);
            return View(motorcycle_Operation);
        }

        // GET: Motorcycle_Operation/Edit/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorcycle_Operation motorcycle_Operation = db.Motorcycle_Operation.Find(id);
            if (motorcycle_Operation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", motorcycle_Operation.CID);
            return View(motorcycle_Operation);
        }

        // POST: Motorcycle_Operation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Edit([Bind(Include = "CoID,CID,TimeDate,MotorcycleType")] Motorcycle_Operation motorcycle_Operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(motorcycle_Operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CID = new SelectList(db.Signs, "CID", "CID", motorcycle_Operation.CID);
            return View(motorcycle_Operation);
        }

        // GET: Motorcycle_Operation/Delete/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motorcycle_Operation motorcycle_Operation = db.Motorcycle_Operation.Find(id);
            if (motorcycle_Operation == null)
            {
                return HttpNotFound();
            }
            return View(motorcycle_Operation);
        }

        // POST: Motorcycle_Operation/Delete/5
        [CustomAuth(ResourceKey = "SomeResource", OperationKey = "SomeAction")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Motorcycle_Operation motorcycle_Operation = db.Motorcycle_Operation.Find(id);
            db.Motorcycle_Operation.Remove(motorcycle_Operation);
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
