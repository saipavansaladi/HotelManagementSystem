using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class customerlogin
    {
        [Display(Name ="Customer ID")]
        public string customerid { get; set; }
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string password { get; set; } 
    }
}
