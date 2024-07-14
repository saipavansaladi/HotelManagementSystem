using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class customerModel
    {
        [Display(Name ="Customer ID")]
        public string customerid { get; set; }
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name ="First Name")]
        [RegularExpression("[A-Za-z]+",ErrorMessage ="Please Enter Valid First Name")]
        public string firstname { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Please Enter Valid Last Name")]
        public string lastname { get; set; }
        [Display(Name ="Gender")]
        public string gender { get; set; }
        [Display(Name ="Email")]
        [EmailAddress(ErrorMessage ="Please Enter Valid Email")]
        public string email { get; set; }
        [Display(Name ="Phone")]
        [Phone(ErrorMessage ="Please Enter Valid Phone Number")]
        public string phone { get; set; }
    }
}
