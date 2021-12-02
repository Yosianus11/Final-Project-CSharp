using System.ComponentModel.DataAnnotations;

namespace PaymentsApi.Models.DTOs.Requests
{
    public class GetUser
    {
        [Required]
        public string Email { get; set; }
    }
}