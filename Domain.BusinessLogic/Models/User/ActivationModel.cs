using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class ActivationModel
    {
        [Required]
        public string Token { get; set; }
    }
}
