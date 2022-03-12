using Common.DataAccess;
using Payment.DataAccess.Enitities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.UserManagement
{
    public class Company : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }

        public int? SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }

        public ICollection<User> Employees { get; set; }
    }
}
