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

namespace MyNote.BussinessLayer
{
    public class NoteUserManager
    {
        private static Repository<NoteUser> _repoUser = new Repository<NoteUser>();

        //yeni kullanıcı kayıt işlemleri
        public static BussinesLayerResult<NoteUser> RegisterUser(RegisterViewModel model)
        {
            BussinesLayerResult<NoteUser> layerResult = new BussinesLayerResult<NoteUser>();

            NoteUser user = _repoUser.Find(m => m.Username == model.Username || m.Email == model.Email);
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
                int dbResult = _repoUser.Insert(new NoteUser()
                {
                    Email = model.Email,
                    Password = model.Password,
                    Username = model.Username,
                    IsActive = false,
                    ActivateGuid = Guid.NewGuid(),
                    IsAdmin=false
                });

                if (dbResult > 0)
                {
                    layerResult.Result = _repoUser.Find(m => m.Email == model.Email && m.Username == model.Username);
                    // TODO :  aktivasyon mail' i atılacak
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{layerResult.Result.ActivateGuid}";
                    string body = $"Hesabınızı aktfi etmek için <a href='{activateUri}'>tıklayınız</a>";
                    MailHelper.SendMail(body,layerResult.Result.Email,"Hesap Aktivasyon",true);

                }
            }

            return layerResult;
        }

        public static BussinesLayerResult<NoteUser> LoginUser(LoginViewModel model)
        {


            BussinesLayerResult<NoteUser> layerResult = new BussinesLayerResult<NoteUser>();
            layerResult.Result = _repoUser.Find(m => m.Username == model.UserName && m.Password == model.Password);

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

        public static BussinesLayerResult<NoteUser> ActiveUser(Guid code)
        {
            BussinesLayerResult<NoteUser> resultLayer = new BussinesLayerResult<NoteUser>();
            resultLayer.Result = _repoUser.Find(m => m.ActivateGuid == code);

            if(resultLayer.Result!=null)
            {
                if(resultLayer.Result.IsActive)
                {
                    resultLayer.AddError(ErrorMessageCode.KullaniciAkitf, "Üye zaten aktif");
                    return resultLayer;
                }
                resultLayer.Result.IsActive = true;
                _repoUser.Update(resultLayer.Result);
            }else
            {
                resultLayer.AddError(ErrorMessageCode.AktiveteIdyok, "Aktivete kodu yok");

            }


            return resultLayer;
        }
    }
}
