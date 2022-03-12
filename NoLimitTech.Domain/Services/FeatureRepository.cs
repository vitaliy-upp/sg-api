﻿using Payment.DataAccess.Enitities;
using System.Linq;

namespace NoLimitTech.Domain.Services
{
    public class FeatureRepository : DomainService<Feature, int>, IFeatureRepository
    {
        public FeatureRepository(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public Feature GetByKey(string key)
        {
            return Context.Set<Feature>()
                .FirstOrDefault(x => x.Key == key);
        }
    }
}
