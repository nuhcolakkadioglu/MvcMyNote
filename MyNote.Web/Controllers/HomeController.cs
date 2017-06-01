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

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if(ModelState.IsValid)
            {
                NoteUser user = null;

                //try
                //{
                //    user= NoteUserManager.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{

                //    ModelState.AddModelError("", ex.Message);
                //}


                //foreach (var item in ModelState)
                //{
                //    if(item.Value.Errors.Count>0)
                //    {
                //         return View(model);
                //    }
                //}

                //if (user == null)
                //    return View(model);

                return RedirectToAction("RegisterOk");
            }

            // kullanıcı adı ve e-posta  kontrolü
            //aktivasyon e-posta gönderimi
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