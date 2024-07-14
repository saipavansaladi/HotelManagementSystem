using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace HotelManagement.Models
{
    public class RoomsModel
    {
        [Display(Name ="Room ID")]
        public int? roomid { get; set; }
        [Display(Name ="Room Number")]
        [Required(ErrorMessage ="Please Enter Valid Room Number")]
        [Range(1,100,ErrorMessage ="Please Enter Valid Room Number")]
        public int roomnumber { get; set; }
        [Display(Name ="Room Type")]
        public string roomtype { get; set; }
        [Display(Name ="Images")]
        public string? imagepath { get; set; }
        [Display(Name ="Facilities")]
        public string? facilities { get; set; }
        [Display(Name ="Cost")]
        public int? cost { get; set; }
        public bool wifi { get; set; }
        public bool minibar { get; set; }
        public bool workspace { get; set; }
        public bool roomservice { get; set; }
    }
}
