using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class feedbackModel
    {
        [Display(Name="Comments")]
        public string comments { get; set; }
    }
}
