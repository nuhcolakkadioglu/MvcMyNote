using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNote.Enties.DTO
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} bu alan zorunlu")]
        public string Username { get; set; }
        [DisplayName("Email"),
            Required(ErrorMessage = "{0} bu alan zorunlu"),
            EmailAddress(ErrorMessage = "{0} alanı için geçerli bir eposta adresi giriniz")]
        public string Email { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} bu alan zorunlu")]
        public string Password { get; set; }
        [DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} bu alan zorunlu"),
            Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor")]
        public string RePassword { get; set; }
    }
}