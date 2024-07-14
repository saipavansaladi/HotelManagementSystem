using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class passwordModel
    {
        [Display(Name ="Enter Current Password")]
        [DataType(DataType.Password)]
        public string currentpassword { get; set; }
        [Display(Name ="Enter New Password")]
        [DataType(DataType.Password)]
        public string newpassword { get; set; }
        [Display(Name ="Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("newpassword",ErrorMessage ="New Password and Confirm New Password fields are not same")]
        public string confirmnewpassword { get; set; }
    }
}
