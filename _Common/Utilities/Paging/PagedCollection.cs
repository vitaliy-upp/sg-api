using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Paging;

namespace Utilities
{
    public class PagedCollection<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public static PagedCollection<T> Create(IList<T> items, EventFilter filter)
        {
            return new PagedCollection<T>()
            {
                PageNumber = filter.Page,
                PageSize = filter.Limit,
                Total = items.Count,
                Items = items.Skip(filter.Limit * (filter.Page - 1)).Take(filter.Limit).ToList()
            };
        }

        public static PagedCollection<T> Create(IQueryable<T> items, EventFilter filter)
        {
            return new PagedCollection<T>()
            {
                PageNumber = filter.Page,
                PageSize = filter.Limit,
                Total = items.Count(),
                Items = items.Skip(filter.Limit * (filter.Page - 1)).Take(filter.Limit).ToList()
            };
        }
    }
}
