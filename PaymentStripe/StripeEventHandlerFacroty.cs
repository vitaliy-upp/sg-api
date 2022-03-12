using Microsoft.AspNetCore.Http;
using PaymentStripe.Attributes;
using PaymentStripe.EventHandlers;
using PaymentStripe.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PaymentStripe
{
    public class StripeEventHandlerFacroty : IStripeEventHandlerFacroty
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        private IEnumerable<Type> _eventHandlerTypes;
        private IEnumerable<Type> EventHandlerTypes 
        { 
            get {
                if(_eventHandlerTypes == null)
                { FindEventHandlerTypes(); }

                return _eventHandlerTypes;
            }
        }


        public StripeEventHandlerFacroty(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IBaseStripeEventHandler Create(Event stripeEvent)
        {
            foreach (var handlerType in EventHandlerTypes)
            {
                var attr = handlerType.GetCustomAttribute(typeof(EventTypeAttribute));
                var val = attr.GetType().GetProperty("Type").GetValue(attr, null).ToString();
                if (val == stripeEvent.Type)
                {
                    return Activator.CreateInstance(handlerType, stripeEvent, _httpContextAccessor) as IBaseStripeEventHandler;
                }
            }

            return new BaseStripeEventHandler<object>(stripeEvent, _httpContextAccessor);
        }

        private void FindEventHandlerTypes()
        {
            _eventHandlerTypes = GetType().Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(EventTypeAttribute)).Count() > 0);
        }
    }
}
