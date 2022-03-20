using Domain.BusinessLogic.Enums;
using Domain.DataAccess.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class CreateInviteModel
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public IList<EventParticipant> Participants { get; set; }
    }

    public class EventParticipant
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public UserRolesEnum Role { get; set; }
    }
}
