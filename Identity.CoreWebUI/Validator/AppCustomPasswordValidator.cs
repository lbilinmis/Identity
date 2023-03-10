using Identity.CoreWebUI.Context;
using Microsoft.AspNetCore.Identity;

namespace Identity.CoreWebUI.Validator
{
    public class AppCustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            // Parolamız kullanıcı adı içermemesini sağlayalım
            //bu işlemi yaptıktan sonra program.cs dosyasına bunun eklenmesi lazım
            List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError() { Code = "PasswordContainsUserName", Description = "Parola kullanıcı adını içermemelidir" });
            }

            if (errors.Count > 0)
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            else
            {
                return Task.FromResult(IdentityResult.Success);
            }
        }
    }
}
