using System;
using DataAccess.UserManagement;
using Domain.DataAccess.ServiceInterfaces;

namespace Domain.DataAccess.Services
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
