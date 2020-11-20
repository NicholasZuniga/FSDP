using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSDP.DATA.EF;

namespace FSDP.UI.MVC.Controllers
{
    public class OwnerDetailsController : Controller
    {
        private FSDPEntities db = new FSDPEntities();

        // GET: OwnerDetails
        public ActionResult Index()
        {
            return View(db.OwnerDetails.ToList());
        }

        // GET: OwnerDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerDetail ownerDetail = db.OwnerDetails.Find(id);
            if (ownerDetail == null)
            {
                return HttpNotFound();
            }
            return View(ownerDetail);
        }

        // GET: OwnerDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OwnerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerId,FirstName,LastName")] OwnerDetail ownerDetail)
        {
            if (ModelState.IsValid)
            {
                db.OwnerDetails.Add(ownerDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ownerDetail);
        }

        // GET: OwnerDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerDetail ownerDetail = db.OwnerDetails.Find(id);
            if (ownerDetail == null)
            {
                return HttpNotFound();
            }
            return View(ownerDetail);
        }

        // POST: OwnerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerId,FirstName,LastName")] OwnerDetail ownerDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ownerDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ownerDetail);
        }

        // GET: OwnerDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerDetail ownerDetail = db.OwnerDetails.Find(id);
            if (ownerDetail == null)
            {
                return HttpNotFound();
            }
            return View(ownerDetail);
        }

        // POST: OwnerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OwnerDetail ownerDetail = db.OwnerDetails.Find(id);
            db.OwnerDetails.Remove(ownerDetail);
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
