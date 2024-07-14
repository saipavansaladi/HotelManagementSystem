using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class adminModel
    {
        [Display(Name ="User ID")]
        public string userid { get; set; }
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
