using Domain.DataAccess;
using Domain.DataAccess.Entities.KidProfile;
using Domain.Repository.Interfaces.KidProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repository.Repository.KidProfile
{
    public class SuperPowerRepository : DomainRepository<SuperPower, int>, ISuperPowerRepository
    {
        public SuperPowerRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }

    }
}
