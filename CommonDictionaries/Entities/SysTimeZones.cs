using Common.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace CommonDictionaries.Entities
{
    public class SysTimeZones : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public string SysTimeZoneId { get; set; }
        public string DisplayName { get; set; }
        public string TzDatabaseName { get; set; }

        //public string UsTimeZone { get; set; }
        //public string TimeZoneValue { get; set; }
        //public string Terriories { get; set; }
        //public string StandarTime { get; set; }
        //public int UTC { get; set; }
    }
}
