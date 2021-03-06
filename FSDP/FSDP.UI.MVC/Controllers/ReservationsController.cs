﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSDP.DATA.EF;
using Microsoft.AspNet.Identity;
//using FSDP.UI.MVC.Models;

namespace FSDP.UI.MVC.Controllers
{
    public class ReservationsController : Controller
    {
        private FSDPEntities db = new FSDPEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            if (User.IsInRole("Client"))
            {
                string currentUserID = User.Identity.GetUserId();
                var reservations = db.Reservations.Where(r => r.Vase.OwnerId == currentUserID).Include(r => r.Location).Include(r => r.Vase);
                return View(reservations.ToList());
            }
            else
            {
                var reservations = db.Reservations.Include(r => r.Location);
                return View(reservations.ToList());
            }
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                string currentUserID = User.Identity.GetUserId();
                ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
                ViewBag.VaseID = new SelectList(db.Vases.Where(v => v.OwnerId == currentUserID), "VaseID", "VaseMaterial");

                return View();
            }
            else
            {
                ViewBag.VaseID = new SelectList((db.Vases), "VaseID", "VaseMaterial");
                ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
                return View();
            }
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,VaseID,LocationId,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole("Admin"))
                {
                    byte? limit = 0;

                    var theRecord = db.Locations.FirstOrDefault(r => r.LocationId == reservation.LocationId);

                    limit = theRecord?.ReservationLimit;

                    var records = db.Reservations.Where(r => r.LocationId == reservation.LocationId && r.ReservationDate == reservation.ReservationDate).ToList().Count;

                    if (records >= limit)
                    {
                        ModelState.AddModelError("ReservationDate", "This date is not availble for this location");
                        ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
                        ViewBag.VaseID = new SelectList(db.Vases, "VaseID", "VaseMaterial");
                        return View();
                    }
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            ViewBag.VaseID = new SelectList(db.Vases, "VaseID", "VaseMaterial", reservation.VaseID);

            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            ViewBag.VaseID = new SelectList(db.Vases, "VaseID", "VaseMaterial", reservation.VaseID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,VaseID,LocationId,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            ViewBag.VaseID = new SelectList(db.Vases, "VaseID", "VaseMaterial", reservation.VaseID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
