using DataAccess.UserManagement;

namespace NoLimitTech.Domain.Services
{
    public interface ICompanyRepository
    {
        Company Create(Company company);
        Company GetById(int value);
        void SaveChanges();
        void Update(object company);
    }
}