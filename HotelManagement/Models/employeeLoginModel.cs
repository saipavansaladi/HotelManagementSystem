using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class employeeLoginModel
    {
        [Display(Name ="Staff ID")]
        public string staffid { get; set; }
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
