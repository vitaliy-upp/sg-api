using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models
{
    public class UserHiddenModel
    {
        [Required]
        public bool IsHidden { get; set; }
    }
}
