using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNote.Enties;
using MyNote.Web.Models;
using MyNote.BussinessLayer;

namespace MyNote.Web.Controllers
{
    public class NoteController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();

        public ActionResult Index()
        {

            var notes = noteManager.ListQueryable().Include("Category").
                Include("Owner")
                .Where(m => m.Owner.Id == CurrentSession.User.Id).OrderByDescending(m=>m.Modified);

            return View(notes.ToList());
        }

        public ActionResult Begendiklerim()
        {
            //var notes = noteManager.ListQueryable().Include("LikedUser")
            //    .Include("Note").Where(m=>m.li)
            //   Include("Owner")
            //   .Where(m => m.Owner.Id == CurrentSession.User.Id).OrderByDescending(m => m.Modified);

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(m => m.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoryesFromCache(), "Id", "Title");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("Modified");
            ModelState.Remove("ModifiedUsername");
            note.Owner = CurrentSession.User;
            if (ModelState.IsValid)
            {
                noteManager.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoryesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(m => m.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoryesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Note note)
        {
            if (ModelState.IsValid)
            {

                Note not = noteManager.Find(m=>m.Id==note.Id);
                not.IsDraft = note.IsDraft;
                not.CategoryId = note.CategoryId;
                not.Text = note.Text;
                not.Title = note.Title;

                noteManager.Update(not);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoryesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(m => m.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteManager.Find(m => m.Id == id);
            noteManager.Delete(note);
            return RedirectToAction("Index");
        }

    }
}
