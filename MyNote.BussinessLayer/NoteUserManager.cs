using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNote.DataAccessLayer.EntityFramework;
using MyNote.Enties;
using MyNote.Enties.DTO;
using MyNote.Enties.Messages;

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
                    // layerResult.Result.ActivateGuid  bu kodu email olarak gönder 
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
    }
}
