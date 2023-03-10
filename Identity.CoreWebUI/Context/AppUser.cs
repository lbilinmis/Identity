using Microsoft.AspNetCore.Identity;

namespace Identity.CoreWebUI.Context
{
    public class AppUser:IdentityUser<int>
    {
        public string ImageUrl { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}
