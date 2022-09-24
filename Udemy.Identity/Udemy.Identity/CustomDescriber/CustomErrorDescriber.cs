using Microsoft.AspNetCore.Identity;

namespace Udemy.Identity.CustomDescriber
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = $"Parola en az {length} karakter olabilir"

            };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Şifre en az bir * - / ? karakter içermelidir"
            };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            {
                Code = "DuplicateUserName",
                Description = $"{userName}Bu kullanıcı zaten Kayıtlı"
            };
        }
    }
}
