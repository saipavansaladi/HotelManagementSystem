using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class paymentModel
    {
        [Display(Name ="Card Number")]
        [MinLength(16,ErrorMessage ="Invalid Card Number")]
        [MaxLength(16, ErrorMessage = "Invalid Card Number")]

        [RegularExpression(@"\d+$", ErrorMessage = "Invalid Card Number")]
        public string cardnumber { get; set; }
        [Display(Name ="CVV")]
        [Range(101,999,ErrorMessage ="Invalid CVV")]
        [RegularExpression(@"\d{3}", ErrorMessage = "Invalid CVV")]
        [DataType(DataType.Password)]
        public int cvv { get; set; }
        [Display(Name ="Street Address")]        
        public string streetaddress { get; set; }
        [Display(Name ="City")]
        public string city { get; set; }
        [Display(Name="State")]
        public string state { get; set; }
        [Display(Name ="Pincode")]      
        [RegularExpression(@"\d{5}", ErrorMessage = "Invalid Zip Code")]
        public string zipcode { get; set; }
    }
}
