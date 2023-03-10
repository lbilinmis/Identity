using Microsoft.AspNetCore.Identity;

namespace Identity.CoreWebUI.Validator
{
    public class AppCustomIdentityValidator:IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError() { Code = "PasswordTooShort", Description = $"Parola minimum {length} karakter olmalıdır" };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError() { Code = "PasswordRequiresNonAlphanumeric", Description = $"Parola alpha numeric karakter içermelidir" };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError() { Code = "DuplicateUserName", Description = $" ({userName}) kullanıcı adı kullanılmıştır" };
        }
    }
}
