using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNote.Web.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage ="{0} alanı boş geçilemez")]
        public string UserName { get; set; }
        [DisplayName("Şifre"),Required(ErrorMessage = "{0} alanı boş geçilemez"),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}