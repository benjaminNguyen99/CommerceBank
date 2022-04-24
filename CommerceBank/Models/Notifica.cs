using System.ComponentModel.DataAnnotations;
using CommerceBank.Models;


namespace CommerceBank.Models
{
    public class Notifica
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please choose a valid date.")]
        [DataType(DataType.Date)]

        [Display(Name = "Processing Date")]
        public System.DateTime CreatedDate { get; set; }
        public string UserKey { get; set; }

        [Required]
        [Display(Name = "Notification Type")]
        public string NotificationType { get; set; }

        [Required]
        [Display(Name = "Notification Rule")]
        public string NotificationRule { get; set; }

    }
}
