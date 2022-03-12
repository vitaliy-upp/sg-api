using System;

namespace Utilities.Paging
{
    public class EventFilter : Pagination
    {
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? CategoryId { get; set; }
        public string Search { get; set; }
    }
}
