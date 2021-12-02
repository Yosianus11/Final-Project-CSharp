using System.ComponentModel.DataAnnotations;

namespace PaymentsApi.Models.DTOs.Responses
{
    public class GetUserRespone
    {
        [Required]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}