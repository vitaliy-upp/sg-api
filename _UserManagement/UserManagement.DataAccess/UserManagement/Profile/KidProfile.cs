using System.ComponentModel.DataAnnotations;

namespace UserManagement.DataAccess.UserManagement.Profile
{
    public class KidProfile
    {
        [Key]
        public int Id { get; set; }

        public int ParrentUserId { get; set; }

        public string Link { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }

    }
}
