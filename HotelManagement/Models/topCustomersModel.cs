using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class topCustomersModel
    {
        [Display(Name ="Customer ID")]
        public int customerid { get; set; }

        [Display(Name ="Total Bookings")]
        public int totalbookings { get; set; }
    }
}
