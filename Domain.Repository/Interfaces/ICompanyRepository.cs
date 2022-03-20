using DataAccess.UserManagement;

namespace Domain.DataAccess.Services
{
    public interface ICompanyRepository
    {
        Company Create(Company company);
        Company GetById(int value);
        void SaveChanges();
        void Update(object company);
    }
}