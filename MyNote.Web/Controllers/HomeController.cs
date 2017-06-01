using MyNote.BussinessLayer;
using MyNote.Enties;
using MyNote.Enties.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyNote.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Hom
        public ActionResult Index()
        {
            //if (TempData["mm"]!=null)
            //{
            //    return View(TempData["mm"] as List<Note>);

            //}

            return View(NoteManager.GetAllNote().OrderByDescending(m => m.Modified).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Category cat = CategoryManager.GetCategoryById(id.Value);
            if (cat == null)
                return HttpNotFound();

            return View("Index",cat.Notes.OrderByDescending(m=>m.Modified).ToList());
        }

        public ActionResult EnBegeni()
        {

            return View("Index",NoteManager.GetAllNote().OrderByDescending(m => m.LikeCount).ToList());
        }

        public ActionResult Hakkimda()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
           
            if(ModelState.IsValid)
            {
                BussinesLayerResult<NoteUser> user = NoteUserManager.LoginUser(model);

                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(m => ModelState.AddModelError("", m));
                    return View(model);
                }
                Session["login"] = user;
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        // kullanıcı kayıt 
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if(ModelState.IsValid)
            {
                BussinesLayerResult<NoteUser> user = new BussinesLayerResult<NoteUser>();
                user = NoteUserManager.RegisterUser(model);

                if(user.Errors.Count>0)
                {
                    user.Errors.ForEach(m => ModelState.AddModelError("", m));
                    return View(model);
                }
             
                return RedirectToAction("RegisterOk");
            }

           
            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivate(Guid activate_id)
        {
            //kullanıcı kayıt sonrası aktif
            return View();
        }

    }
}