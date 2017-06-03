using MyNote.BussinessLayer;
using MyNote.BussinessLayer.Results;
using MyNote.Enties;
using MyNote.Enties.DTO;
using MyNote.Enties.Messages;
using MyNote.Web.Models;
using MyNote.Web.ViewModels;
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
        private CategoryManager categoryManager = new CategoryManager();
        private NoteUserManager noteUserManager = new NoteUserManager();
        private NoteManager noteManager = new NoteManager();

        public ActionResult Index()
        {
            //if (TempData["mm"]!=null)
            //{
            //    return View(TempData["mm"] as List<Note>);

            //}

            return View(noteManager.ListQueryable().OrderByDescending(m => m.Modified).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Category cat = categoryManager.Find(m => m.Id == id);
            if (cat == null)
                return HttpNotFound();

            return View("Index", cat.Notes.OrderByDescending(m => m.Modified).ToList());
        }

        public ActionResult EnBegeni()
        {

            return View("Index", noteManager.ListQueryable().OrderByDescending(m => m.LikeCount).ToList());
        }

        public ActionResult Hakkimda()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        //login post
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                BussinesLayerResult<NoteUser> user = noteUserManager.LoginUser(model);
                NoteUser session = user.Result;
                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(m => ModelState.AddModelError("", m.Message));

                    if (user.Errors.Find(m => m.Code == ErrorMessageCode.UserAktifDegil) != null)
                    {
                        ViewBag.Link = "/Home/UserActivate";
                    }

                    return View(model);
                }
                CurrentSession.Set<NoteUser>("login", session);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        // kullanıcı kayıt 
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                BussinesLayerResult<NoteUser> user = new BussinesLayerResult<NoteUser>();
                user = noteUserManager.RegisterUser(model);

                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(m => ModelState.AddModelError("", m.Message));
                    return View(model);
                }
                OkViewModel okModel = new OkViewModel();
                okModel.Title = "Kayıt Başarılı";
                okModel.Header = "Kayıt oldunuz";
                okModel.RedirectingUrl = "/Home/Login";
                okModel.Items.Add("Eposta adresinize gönderilen aktivasyon linki ile hesabınızı lütfen aktif ediniz.");

                return View("Ok", okModel);
            }


            return View(model);
        }



        public ActionResult UserActivate(Guid id)
        {
            BussinesLayerResult<NoteUser> user = noteUserManager.ActiveUser(id);
            if (user.Errors.Count > 0)
            {
                ErrorViewModel errorModel = new ErrorViewModel()
                {
                    Header = "Hesap geçersiz",
                    Title = "Geçersiz İşlem",
                    Items = user.Errors

                };

                return View("Error", errorModel);
            }

            OkViewModel OkViewModel = new OkViewModel()
            {
                Title = "Hesabınız Aktif Edilmiştir",
                RedirectingUrl = "/Home/Login"

            };
            OkViewModel.Items.Add("Hesabınız Aktif Edilmiştir");
            return View("Ok", OkViewModel);
        }



        public ActionResult ShowProfile()
        {

            BussinesLayerResult<NoteUser> res = noteUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorModel = new ErrorViewModel()
                {
                    Title = "Kullanıcı bulunamadı",
                    Items = res.Errors
                };

                return View("Error", errorModel);
            }

            return View(res.Result);
        }

        public ActionResult EditProfile()
        {
            BussinesLayerResult<NoteUser> res = noteUserManager.GetUserById(CurrentSession.User.Id);


            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorModel = new ErrorViewModel()
                {
                    Title = "lütfen önce oturum açınız",
                    Items = res.Errors
                };
                return View("Error", errorModel);
            }


            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(NoteUser model, HttpPostedFileBase ProfileImageFileName)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (ProfileImageFileName != null &&
                (ProfileImageFileName.ContentType == "image/jpeg") || ProfileImageFileName.ContentType == "image/jpg" ||
                    ProfileImageFileName.ContentType == "iamage/png")
                {
                    string filename = $"user_{model.Id}.{ProfileImageFileName.ContentType.Split('/')[1]}";
                    ProfileImageFileName.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    model.ProfileImageFileName = filename;
                }

                BussinesLayerResult<NoteUser> res = noteUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel messages = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "profil güncellenemedi",
                        RedirectingUrl = "/Home/EditProfile"
                    };
                    return View("Error", messages);
                }
                NoteUser session = new NoteUser();
                session = res.Result;
                CurrentSession.Set<NoteUser>("login", session);


                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }

        public ActionResult DeleteProfile()
        {
            NoteUser currentUser = CurrentSession.User;
            BussinesLayerResult<NoteUser> res = noteUserManager.RemoveUser(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorModel = new ErrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profil silinemedi",
                    RedirectingUrl = "/Home/ShowProfile"
                };
                return View("Error", errorModel);
            }

            return RedirectToAction("Login");
        }

    }
}