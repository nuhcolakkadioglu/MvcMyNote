using MyNote.BussinessLayer;
using MyNote.Enties;
using MyNote.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNote.Web.Controllers
{
    public class CommentController : Controller
    {
        private NoteManager notManager = new NoteManager();
        private CommentManager commentManager = new CommentManager();

        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Note not = notManager.Find(m => m.Id == id);

            if (not == null)
                return HttpNotFound();

            return PartialView("_PartialComments", not.Comments.ToList());
        }

        [HttpPost]
        public ActionResult Edit(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Comment yorum = commentManager.Find(m => m.Id == id);

            if (yorum == null)
            {
                return HttpNotFound();
            }

            yorum.Text = text;
            if (commentManager.Update(yorum) > 0)
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Comment yorum = commentManager.Find(m => m.Id == id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            if (commentManager.Delete(yorum) > 0)
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create(Comment model,int? noteid)
        {
            ModelState.Remove("ModifiedUsername");
            ModelState.Remove("Modified");
            ModelState.Remove("CreatedOn");

            if (ModelState.IsValid)
            {
                if (noteid == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                Note not = notManager.Find(m => m.Id == noteid);
                if (not == null)
                {
                    return HttpNotFound();
                }
                model.Note = not;
                model.Owner = CurrentSession.User;

                if (commentManager.Insert(model) > 0)
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}