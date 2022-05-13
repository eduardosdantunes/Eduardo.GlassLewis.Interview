using Interview.Domain.Entities;

namespace Interview.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> CreateCompanyAsync(Company company, CancellationToken cancellationToken);
        Task<Company?> FindByIdAsync(int id, CancellationToken cancellationToken);
        Task<Company?> FindByIsinAsync(string isin, CancellationToken cancellation);
        IAsyncEnumerable<Company> GetAllCompanies(CancellationToken cancellation);
        Task<Company?> SaveChangesAsync(int id, Company company, CancellationToken cancellationToken);

    }
}