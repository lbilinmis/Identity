using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Identity.CoreWebUI.Models
{
    public class ViewModelAppRole
    {
        [Display(Name = "Role Adı")]
        [Required(ErrorMessage = "Role Ad alanı gereklidir.")]
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
