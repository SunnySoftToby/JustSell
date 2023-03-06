using JustSell.Models;

namespace JustSell.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);
    }
}

