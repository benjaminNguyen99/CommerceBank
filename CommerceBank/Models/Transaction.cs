using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace CommerceBank.Models
{
    public enum SortTransaction { Ascending = 0, Descending = 1 }
    
    public class Transaction
    {

        
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "Please choose a valid date.")]
        [DataType(DataType.Date)]
        
        [Display(Name = "Processing Date")]
        public System.DateTime? CreatedDate { get; set; }

        [Required]
        public float Balance { get; set; }
        [Required (ErrorMessage = "Please choose a valid type (Deposit or Withdrawal).")]
        [Display(Name = "Type")]
        public string BalanceType { get; set; }
        [Required(ErrorMessage = "Please choose a valid location.")]
        public string Location { get; set; }

        [Required (ErrorMessage = "Description must be included.")]
        public string Description   { get; set; }

        public string UserKey { get; set; }


        [Required]
        [Range (1, float.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public float Amount { get; set; }

        

    }
}
