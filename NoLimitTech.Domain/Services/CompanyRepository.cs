using System;
using DataAccess.UserManagement;
using NoLimitTech.Domain.ServiceInterfaces;

namespace NoLimitTech.Domain.Services
{
    public class CompanyRepository : DomainService<Company, int>, ICompanyRepository
    {
        public CompanyRepository(DomainDbContext dbContext)
            : base(dbContext)
        {

        }

        public void Update(object company)
        {
            throw new NotImplementedException();
        }
    }
}
