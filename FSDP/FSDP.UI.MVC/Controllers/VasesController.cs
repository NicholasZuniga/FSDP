using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using FSDP.DATA.EF;
using FSDP.UI.MVC.Utilities;
using Microsoft.AspNet.Identity;

namespace FSDP.UI.MVC.Controllers
{
    public class VasesController : Controller
    {
        private FSDPEntities db = new FSDPEntities();

        // GET: Vases
        public ActionResult Index()
        {
            if (User.IsInRole("Client"))
            {
                string userid = GetUserId();
                var vases = db.Vases.Where(v => v.OwnerId == userid);
                return View(vases.ToList());
            }
            else
            {
                var vases = db.Vases.Include(v => v.OwnerDetail);
                return View(vases.ToList());
            }
        }

        private string GetUserId()
        {
            var claimuser = (ClaimsPrincipal)User;
            var useridclaim = claimuser.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var userid = useridclaim.Value;
            return userid;
        }

        // GET: Vases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vase vase = db.Vases.Find(id);
            if (vase == null)
            {
                return HttpNotFound();
            }
            return View(vase);
        }

        // GET: Vases/Create
        public ActionResult Create()
        {
            var userId = GetUserId();
            ViewBag.OwnerId = new SelectList(db.OwnerDetails, "OwnerId", "FirstName", userId);
            return View();
        }

        // POST: Vases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VaseID,VaseMaterial,OwnerId,VasePhoto,SpecialNotes,IsActive,DateAdded")] Vase vase, HttpPostedFileBase vaseImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";
                if (vaseImage != null)
                {
                    file = vaseImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    //check that the uploaded file ext is in our list of good file extensions
                    if (goodExts.Contains(ext))
                    {
                        //if valid ext, check file size <= 4mb (max by default from ASP.NET)
                        if (vaseImage.ContentLength <= 4194304)
                        {
                            //create a new file name using a guid
                            //file = Guid.NewGuid() + ext;

                            #region Resize Image
                            string savePath = Server.MapPath("~/Content/img/vaseimages/");
                            Image convertedImage = Image.FromStream(vaseImage.InputStream);
                            int maxImageSize = 500;
                            int maxThumbSize = 100;
                            ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                            #endregion
                        }
                    }
                }
                vase.VasePhoto = file;
                #endregion

                db.Vases.Add(vase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.OwnerDetails, "OwnerId", "FirstName", vase.OwnerId);
            return View(vase);
        }

        // GET: Vases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vase vase = db.Vases.Find(id);
            if (vase == null)
            {
                return HttpNotFound();
            }
            var userId = GetUserId();
            ViewBag.OwnerId = new SelectList(db.OwnerDetails, "OwnerId", "FirstName", userId);
            return View(vase);
        }

        // POST: Vases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VaseID,VaseMaterial,OwnerId,VasePhoto,SpecialNotes,IsActive,DateAdded")] Vase vase, HttpPostedFileBase vaseImage)

        {
            
            if (ModelState.IsValid)
            {
                
                #region File Upload
                if (vaseImage != null)
                {
                    string file = vaseImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    //check that the uploaded file ext is in our list of good file extensions
                    if (goodExts.Contains(ext))
                    {
                        //if valid ext, check file size <= 4mb (max by default from ASP.NET)
                        if (vaseImage.ContentLength <= 4194304)
                        {
                            //create a new file name using a guid
                            //file = Guid.NewGuid() + ext;

                            #region Resize Image
                            string savePath = Server.MapPath("~/Content/img/vaseimages/");
                            Image convertedImage = Image.FromStream(vaseImage.InputStream);
                            int maxImageSize = 500;
                            int maxThumbSize = 100;
                            ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                            #endregion

                            if (vase.VasePhoto != null && vase.VasePhoto != "NoImage.png")
                            {
                                string path = Server.MapPath("~/Content/img/vaseimages/");
                                ImageService.Delete(path, vase.VasePhoto);
                            }
                        }
                    }
                    vase.VasePhoto = file;
                }
                #endregion

                db.Entry(vase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.OwnerDetails, "OwnerId", "FirstName", vase.OwnerId);
            return View(vase);
        }

        // GET: Vases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vase vase = db.Vases.Find(id);
            if (vase == null)
            {
                return HttpNotFound();
            }
            return View(vase);
        }

        // POST: Vases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vase vase = db.Vases.Find(id);

            //Delete the image file of the record that is being removed
            string path = Server.MapPath("~/Content/img/vaseimages/");
            ImageService.Delete(path, vase.VasePhoto);

            db.Vases.Remove(vase);
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
