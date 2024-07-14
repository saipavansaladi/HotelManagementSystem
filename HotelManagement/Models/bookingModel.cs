using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HotelManagement.Models
{
    public class bookingModel
    {
        [Display(Name="Booking ID")]
        public int bookingid { get; set; }
        [Display(Name = "Customer ID")]
        public string customerid { get; set; }
        [Display(Name = "Room ID")]
        public int roomid { get; set; }
        [Display(Name = "Booking Start Date")]
        public DateTime bookingstartdate { get; set; }
        [Display(Name = "Booking End Date")]
        public DateTime bookingenddate { get; set; }
        [Display(Name = "Total Cost")]
        public float totalcost { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
    }
}
