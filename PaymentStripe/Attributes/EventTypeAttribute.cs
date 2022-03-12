using System;

namespace PaymentStripe.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EventTypeAttribute : Attribute
    {
        public string Type { get; set; }
    }
}
