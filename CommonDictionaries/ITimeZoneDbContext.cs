using DataAccess.UserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using CommonDictionaries.Entities;

namespace CommonDictionaries
{
    public interface ITimeZoneDbContext
    {
        DbSet<SysTimeZones> TimeZones { get; set; }
    }
}
