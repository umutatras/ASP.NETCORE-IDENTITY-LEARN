using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage ="kullanıcı adı gereklidir")]
        public string Username { get; set; }
        [Required(ErrorMessage = "şifre  gereklidir")]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
