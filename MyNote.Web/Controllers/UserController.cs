using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNote.Enties;
using MyNote.BussinessLayer;
using MyNote.BussinessLayer.Results;

namespace MyNote.Web.Controllers
{
    public class UserController : Controller
    {
        private NoteUserManager noteuserManager = new NoteUserManager();
        public ActionResult Index()
        {
            return View(noteuserManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteUser noteUser = noteuserManager.Find(m=>m.Id==id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteUser noteUser)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {

                BussinesLayerResult<NoteUser> res = noteuserManager.Insert(noteUser);

                if(res.Errors.Count>0)
                {
                    res.Errors.ForEach(m => ModelState.AddModelError("", m.Message));
                    return View(noteUser);
                }

                return RedirectToAction("Index");
            }

            return View(noteUser);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteUser noteUser = noteuserManager.Find(m=>m.Id==id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( NoteUser noteUser)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BussinesLayerResult<NoteUser> res = noteuserManager.Update(noteUser);

                if(res.Errors.Count>0)
                {
                    res.Errors.ForEach(m => ModelState.AddModelError("", m.Message));
                    return View(noteUser);
                }


                return RedirectToAction("Index");
            }
            return View(noteUser);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteUser noteUser = noteuserManager.Find(m=>m.Id==id);
            if (noteUser == null)
            {
                return HttpNotFound();
            }
            return View(noteUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoteUser noteUser = noteuserManager.Find(m => m.Id == id);
            noteuserManager.Delete(noteUser);
            return RedirectToAction("Index");
        }

 
    }
}
