using Common.DataAccess.Utilities;
using Payment.DataAccess.Enum;
using System;

namespace Payment.DataAccess.Enitities
{
    //public class Transaction: IBaseDomainModel<int>, ICreatedDate
    //{
    //    public int Id { get; set; }
    //    public int UserId { get; set; }
    //    public decimal Price { get; set; }
    //    public int SubcriptionPlanId { get; set; }
    //    public SubscriptionPlan SubcriptionPlan { get; set; }

    //    /// <summary>
    //    /// This could be also an enum.
    //    /// </summary>
    //    public string TransactionStatus { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //}


    public class Transaction : IBaseDomainModel<int>, ICreatedDate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public TransactionProductTypeEnum TransactionProductType { get; set; }
        public int ProductId { get; set; }
        public string TransactionStatus { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class Transaction<T> : Transaction
        where T : class 
    {
        public T Product { get; set; }
    }
}
