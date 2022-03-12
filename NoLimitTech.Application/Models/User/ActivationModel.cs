using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models
{
    public class ActivationModel
    {
        [Required]
        public string Token { get; set; }
    }
}
