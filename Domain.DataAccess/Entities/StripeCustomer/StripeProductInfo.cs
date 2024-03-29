﻿using Common.DataAccess.Utilities;

namespace Domain.DataAccess.Models
{
    public class StripeProductInfo : IBaseDomainModel<int>
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string PriceId { get; set; }
        public int? ConferenceId { get; set; }
        public int? SubscriptionPlanId { get; set; }
    }
}
