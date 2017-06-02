using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNote.DataAccessLayer.EntityFramework;
using MyNote.Enties;
using MyNote.Enties.DTO;
using MyNote.Enties.Messages;
using MyNote.Common.Helpers;
using MyNote.BussinessLayer.Results;
using MyNote.BussinessLayer.Abstact;

namespace MyNote.BussinessLayer
{
    public class NoteUserManager: ManagerBase<NoteUser>
    {

        //yeni kullanıcı kayıt işlemleri
        public  BussinesLayerResult<NoteUser> RegisterUser(RegisterViewModel model)
        {
            BussinesLayerResult<NoteUser> layerResult = new BussinesLayerResult<NoteUser>();
            
            NoteUser user = Find(m => m.Username == model.Username || m.Email == model.Email);
            if (user != null)
            {
                if (user.Username == model.Username)
                {
                    layerResult.AddError(ErrorMessageCode.KullaniciAdiZatenVar, "kullanıcı adı kayıtlı");
                }

                if (user.Email == model.Email)
                {
                    layerResult.AddError(ErrorMessageCode.EmailKayitli, "Email  kayıtlı");
                }

            }
            else
            {
                int dbResult = Insert(new NoteUser()
                {
                    Email = model.Email,
                    Password = model.Password,
                    Username = model.Username,
                    IsActive = false,
                    ActivateGuid = Guid.NewGuid(),
                    IsAdmin = false,
                    ProfileImageFileName = "User.png"
                });

                if (dbResult > 0)
                {
                    layerResult.Result = Find(m => m.Email == model.Email && m.Username == model.Username);
                    // TODO :  aktivasyon mail' i atılacak
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{layerResult.Result.ActivateGuid}";
                    string body = $"Hesabınızı aktfi etmek için <a href='{activateUri}'>tıklayınız</a>";
                    MailHelper.SendMail(body, layerResult.Result.Email, "Hesap Aktivasyon", true);

                }
            }

            return layerResult;
        }

        public  BussinesLayerResult<NoteUser> LoginUser(LoginViewModel model)
        {


            BussinesLayerResult<NoteUser> layerResult = new BussinesLayerResult<NoteUser>();
            layerResult.Result = Find(m => m.Username == model.UserName && m.Password == model.Password);

            if (layerResult.Result != null)
            {
                if (!layerResult.Result.IsActive)
                {
                    //layerResult.Errors.Add("kullanıcı aktif degil e-postanızdan aktiv ediniz");
                    layerResult.AddError(ErrorMessageCode.UserAktifDegil, "kullanıcı aktif degil e-postanızdan aktiv ediniz");
                }
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.KullaniciAdiVeSifreUyusmuyor, "kullanıcı adı yada şifre hatalı");
            }

            return layerResult;
        }

        public  BussinesLayerResult<NoteUser> ActiveUser(Guid code)
        {
            BussinesLayerResult<NoteUser> resultLayer = new BussinesLayerResult<NoteUser>();
            resultLayer.Result = Find(m => m.ActivateGuid == code);

            if (resultLayer.Result != null)
            {
                if (resultLayer.Result.IsActive)
                {
                    resultLayer.AddError(ErrorMessageCode.KullaniciAkitf, "Üye zaten aktif");
                    return resultLayer;
                }
                resultLayer.Result.IsActive = true;
                Update(resultLayer.Result);
            }
            else
            {
                resultLayer.AddError(ErrorMessageCode.AktiveteIdyok, "Aktivete kodu yok");

            }


            return resultLayer;
        }

        public  BussinesLayerResult<NoteUser> GetUserById(int id)
        {
            BussinesLayerResult<NoteUser> resultLayer = new BussinesLayerResult<NoteUser>();

            resultLayer.Result = Find(m => m.Id == id);

            if (resultLayer.Result == null)
            {
                resultLayer.AddError(ErrorMessageCode.KullaniciBulunamadi, "Kullanıcı bulunamadı");
            }

            return resultLayer;
        }

        public  BussinesLayerResult<NoteUser> UpdateProfile(NoteUser model)
        {

            NoteUser db_user = Find(m => m.Username == model.Username && m.Email == model.Email);
            BussinesLayerResult<NoteUser> res = new BussinesLayerResult<NoteUser>();
            if (db_user != null && db_user.Id != model.Id)
            {
                if (db_user.Username == model.Username)
                {
                    res.AddError(ErrorMessageCode.KullaniciAdiZatenVar, "kullanıcı adı kayıtlı");
                }
                if (db_user.Email == model.Email)
                {
                    res.AddError(ErrorMessageCode.EmailKayitli, "eposta adresi kayıtlı");

                }
                return res;
            }

            res.Result = Find(m => m.Id == model.Id);
            res.Result.Email = model.Email;
            res.Result.Name = model.Name;
            res.Result.Surname = model.Surname;
            res.Result.Password = model.Password;
            res.Result.Username = model.Username;

            if (string.IsNullOrEmpty(model.ProfileImageFileName) == false)
            {
                res.Result.ProfileImageFileName = model.ProfileImageFileName;
            }
            if (Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.profilGuncellenemedi, "Profil güncellenemedi");
            }

            return res;
        }

        public  BussinesLayerResult<NoteUser> RemoveUser(int Id)
        {

            BussinesLayerResult<NoteUser> res = new BussinesLayerResult<NoteUser>();
            NoteUser user = Find(m => m.Id == Id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.KullanicSilinemedi, "kullanıcı silinemedi");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.KullaniciBulunamadi, "kullanıcı bulunamadı");

            }

            return res;

        }
    }
}
