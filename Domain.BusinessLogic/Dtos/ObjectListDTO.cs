using System.Collections.Generic;

namespace Domain.BusinessLogic.Models
{
    public class ObjectListDTO<T>
    {
        public List<T> Items { get; set; } 
    }
}
