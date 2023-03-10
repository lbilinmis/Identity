using System.ComponentModel.DataAnnotations;

namespace Identity.CoreWebUI.Models
{
    public class ViewModelLogInUser
    {
        [Display(Name ="Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Parola boş geçilemez")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
