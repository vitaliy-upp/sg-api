using CommonDictionaries.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoLimitTech.Domain.Configurations
{
    public class SysTimeZonesConfiguration : IEntityTypeConfiguration<SysTimeZones>
    {
        public void Configure(EntityTypeBuilder<SysTimeZones> builder)
        {
            builder.HasData(
                new SysTimeZones { Id = 1, SysTimeZoneId = "SA Western Standard Time", DisplayName = "(UTC-04:00) Georgetown, La Paz, Manaus, San Juan", TzDatabaseName = "America/La_Paz" },
                new SysTimeZones { Id = 2, SysTimeZoneId = "Central Brazilian Standard Time", DisplayName = "(UTC-04:00) Cuiaba", TzDatabaseName= "America/Cuiaba" },
                new SysTimeZones { Id = 3, SysTimeZoneId = "Venezuela Standard Time", DisplayName = "(UTC-04:00) Caracas", TzDatabaseName= "America/Caracas" },
                new SysTimeZones { Id = 4, SysTimeZoneId = "Atlantic Standard Time", DisplayName = "(UTC-04:00) Atlantic Time (Canada)", TzDatabaseName= "America/Halifax" },
                new SysTimeZones { Id = 5, SysTimeZoneId = "Paraguay Standard Time", DisplayName = "(UTC-04:00) Asuncion", TzDatabaseName = "America/Asuncion" },
                new SysTimeZones { Id = 6, SysTimeZoneId = "US Eastern Standard Time", DisplayName = "(UTC-05:00) Indiana (East)", TzDatabaseName = "America/Indiana/Indianapolis" },
                new SysTimeZones { Id = 7, SysTimeZoneId = "Cuba Standard Time", DisplayName = "(UTC-05:00) Havana", TzDatabaseName = "America/Havana" },
                new SysTimeZones { Id = 8, SysTimeZoneId = "Eastern Standard Time", DisplayName = "(UTC-05:00) Eastern Time (US & Canada)", TzDatabaseName = "America/New_York" },
                new SysTimeZones { Id = 9, SysTimeZoneId = "SA Pacific Standard Time", DisplayName = "(UTC-05:00) Bogota, Lima, Quito, Rio Branco", TzDatabaseName = "America/Bogota" },
                new SysTimeZones { Id = 10, SysTimeZoneId = "Canada Central Standard Time", DisplayName = "(UTC-06:00) Saskatchewan", TzDatabaseName = "America/Regina" },
                new SysTimeZones { Id = 11, SysTimeZoneId = "Easter Island Standard Time", DisplayName = "(UTC-06:00) Easter Island", TzDatabaseName = "Pacific/Easter" },
                new SysTimeZones { Id = 12, SysTimeZoneId = "Central Standard Time", DisplayName = "(UTC-06:00) Central Time (US & Canada)", TzDatabaseName = "America/Chicago" },
                new SysTimeZones { Id = 13, SysTimeZoneId = "Mountain Standard Time", DisplayName = "(UTC-07:00) Mountain Time (US & Canada)", TzDatabaseName = "America/Denver" },
                new SysTimeZones { Id = 14, SysTimeZoneId = "US Mountain Standard Time", DisplayName = "(UTC-07:00) Arizona", TzDatabaseName = "America/Phoenix" },
                new SysTimeZones { Id = 15, SysTimeZoneId = "Pacific Standard Time", DisplayName = "(UTC-08:00) Pacific Time (US & Canada)", TzDatabaseName = "America/Los_Angeles" },
                new SysTimeZones { Id = 16, SysTimeZoneId = "Alaskan Standard Time", DisplayName = "(UTC-09:00) Alaska", TzDatabaseName = "America/Anchorage" },
                new SysTimeZones { Id = 17, SysTimeZoneId = "Hawaiian Standard Time", DisplayName = "(UTC-10:00) Hawaii", TzDatabaseName = "Pacific/Honolulu" }
                
                //new SysTimeZones { Id = 22, SysTimeZoneId = "UTC-09", DisplayName = "(UTC-09:00) Coordinated Universal Time-09", TzDatabaseName = "" },
                //new SysTimeZones { Id = 16, SysTimeZoneId = "Central America Standard Time", DisplayName = "(UTC-06:00) Central America", TzDatabaseName = "" },
                //new SysTimeZones { Id = 13, SysTimeZoneId = "Central Standard Time (Mexico)", DisplayName = "(UTC-06:00) Guadalajara, Mexico City, Monterrey", TzDatabaseName = "" },
                //new SysTimeZones { Id = 18, SysTimeZoneId = "Mountain Standard Time (Mexico)", DisplayName = "(UTC-07:00) Chihuahua, La Paz, Mazatlan", TzDatabaseName = "" },
                //new SysTimeZones { Id = 21, SysTimeZoneId = "Pacific Standard Time (Mexico)", DisplayName = "(UTC-08:00) Baja California", TzDatabaseName = "" },
                //new SysTimeZones { Id = 10, SysTimeZoneId = "Eastern Standard Time (Mexico)", DisplayName = "(UTC-05:00) Chetumal", TzDatabaseName = "" },
                //new SysTimeZones { Id = 8, SysTimeZoneId = "Haiti Standard Time", DisplayName = "(UTC-05:00) Haiti", TzDatabaseName = "" },
                //new SysTimeZones { Id = 18, SysTimeZoneId = "Yukon Standard Time", DisplayName = "(UTC-07:00) Yukon", TzDatabaseName = "" },
                //new SysTimeZones { Id = 23, SysTimeZoneId = "UTC-08", DisplayName = "(UTC-08:00) Coordinated Universal Time-08", TzDatabaseName = "" },
                //new SysTimeZones { Id = 27, SysTimeZoneId = "Marquesas Standard Time", DisplayName = "(UTC-09:30) Marquesas Islands", TzDatabaseName = "" },
                //new SysTimeZones { Id = 6, SysTimeZoneId = "Turks And Caicos Standard Time", DisplayName = "(UTC-05:00) Turks and Caicos", TzDatabaseName = "" },
                //new SysTimeZones { Id = 29, SysTimeZoneId = "Aleutian Standard Time", DisplayName = "(UTC-10:00) Aleutian Islands", TzDatabaseName = "" }
             );

            //builder.HasData(
            //    new TimeZones { Id = 1, StandarTime = "UTC-04:00", Terriories= "Puerto Rico, US Virgin Islands", TimeZoneValue = "America/Puerto_Rico", UTC=-4, UsTimeZone= "Atlantic" },
            //    new TimeZones { Id = 2, StandarTime = "UTC-05:00", Terriories = "Entire: Connecticut, Delaware, Georgia, Maine, Maryland, Massachusetts, New Hampshire, New Jersey, New York, North Carolina, Ohio, Pennsylvania, Rhode Island, South Carolina, Vermont, Virginia, West Virginia Partial: Florida, Indiana, Kentucky, Michigan, Tennessee", TimeZoneValue = "US/Eastern", UTC = -5, UsTimeZone = "Eastern" },
            //    new TimeZones { Id = 3, StandarTime = "UTC-06:00", Terriories = "Entire: Alabama, Arkansas, Illinois, Iowa, Louisiana, Minnesota, Mississippi, Missouri, Oklahoma, Wisconsin; Partial: Florida, Indiana, Kansas, Kentucky, Michigan, Nebraska, North Dakota, South Dakota, Tennessee, Texas", TimeZoneValue = "US/Central", UTC = -6, UsTimeZone = "Central" },
            //    new TimeZones { Id = 4, StandarTime = "UTC-07:00", Terriories = "Entire: Arizona, Colorado, Montana, New Mexico, Utah, Wyoming Partial: Idaho, Kansas, Nebraska, Nevada, North Dakota, Oregon, South Dakota, Texas", TimeZoneValue = "US/Mountain", UTC = -7, UsTimeZone = "Mountain" },
            //    new TimeZones { Id = 5, StandarTime = "UTC-08:00", Terriories = "Entire: California, Washington Partial: Idaho, Oregon", TimeZoneValue = "US/Pacific", UTC = -8, UsTimeZone = "Pacific" },
            //    new TimeZones { Id = 6, StandarTime = "UTC-09:00", Terriories = "Partial: Alaska", TimeZoneValue = "US/Alaska", UTC = -9, UsTimeZone = "Alaska" },
            //    new TimeZones { Id = 7, StandarTime = "UTC-10:00", Terriories = "Entire: Hawaii Partial: Alaska", TimeZoneValue = "US/Hawaii", UTC = -10, UsTimeZone = "Hawaii" }
            //    );
        }
    }
}
