using System;
using DataAccess.UserManagement;
using Domain.DataAccess.ServiceInterfaces;
using Domain.Repository;

namespace Domain.DataAccess.Services
{
    public class CompanyRepository : DomainRepository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public Company GetById(int value)
        {
            throw new NotImplementedException();
        }

        public void Update(object company)
        {
            throw new NotImplementedException();
        }
    }
}
