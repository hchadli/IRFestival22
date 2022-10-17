using System.ComponentModel.DataAnnotations;

namespace IRFestival.Api.Domain
{
    public class Mailer
    {
        [Required(ErrorMessage = "Picture is required")]
        public IFormFile File { get; set; } 

        [Required(ErrorMessage = "Message is required")]
        [MinLength(10, ErrorMessage = "Min length is 10 characters")]
        public string Content { get; set; } 

        [EmailAddress(ErrorMessage = "Email is invalid")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
