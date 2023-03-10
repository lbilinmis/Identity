using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Identity.CoreWebUI.Models
{
    public class ViewModelRegisterUser
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş geçilemez")]

        public string Password { get; set; }
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password",ErrorMessage ="Parola eşlemedi")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Adınız")]
        [Required(ErrorMessage = "Ad boş geçilemez")]
        public string Ad { get; set; }

        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "Soyad boş geçilemez")]
        public string Soyad { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email  boş geçilemez")]
        public string Email { get; set; }
    }
}
