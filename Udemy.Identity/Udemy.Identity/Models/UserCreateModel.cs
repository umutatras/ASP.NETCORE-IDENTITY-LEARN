using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage ="Lütfen alanı doldurunuz")]
        public string Username{ get; set; }
        [Required(ErrorMessage = "Lütfen alanı doldurunuz")]
        [EmailAddress(ErrorMessage ="Geçerli bir mail adresi giriniz")]
        public string Email{ get; set; }
        [Required(ErrorMessage = "Lütfen alanı doldurunuz")]
        public string Password{ get; set; }
        [Required(ErrorMessage = "Lütfen alanı doldurunuz")]
        [Compare("Password", ErrorMessage ="Paralolar eşleşmiyor")]
        public string ConfirmPassword{ get; set; }
        [Required(ErrorMessage = "Lütfen alanı doldurunuz")]
        public string Gender{ get; set; }
    }
}
