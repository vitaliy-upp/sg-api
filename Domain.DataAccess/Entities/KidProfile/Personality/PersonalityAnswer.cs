using Common.DataAccess.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataAccess.Entities.KidProfile
{
    public class PersonalityAnswer
    {
        [Key]
        public int KidId { get; set; }
        [Key]
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
