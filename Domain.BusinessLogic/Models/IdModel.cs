using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class IdModel
    {
        [Required]
        public int Id { get; set; }
    }
}
