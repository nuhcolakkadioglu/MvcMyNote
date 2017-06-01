using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNote.BussinessLayer;
using MyNote.Enties;

namespace MyNote.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Select(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Category cat = CategoryManager.GetCategoryById(id.Value);
            if (cat == null)
                return HttpNotFound();
            TempData["mm"] = cat.Notes;

            return RedirectToAction("Index", "Home");
        }
    }
}