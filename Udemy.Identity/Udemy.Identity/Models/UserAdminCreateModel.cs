using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models
{
    public class UserAdminCreateModel
    {
        [Required(ErrorMessage ="Kullanıcı adı gereklidir")]
        public string Username { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}
