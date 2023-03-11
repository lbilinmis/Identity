using System.ComponentModel.DataAnnotations;

namespace Identity.CoreWebUI.Models
{
    public class ViewModelAppUser
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir email adresi giriniz.")]
        public string Email { get; set; }
        [Display(Name = "Telefon No")]
        public string PhoneNumber { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Adınız")]
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Ad { get; set; }

        [Display(Name = "Soyadınız")]

        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        public string Soyad { get; set; }
    }
}
