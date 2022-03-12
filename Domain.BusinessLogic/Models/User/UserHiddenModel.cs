using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class UserHiddenModel
    {
        [Required]
        public bool IsHidden { get; set; }
    }
}
