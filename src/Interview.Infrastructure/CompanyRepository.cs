using Interview.Domain.Entities;
using Interview.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Interview.Infrastructure
{
    internal class CompanyRepository : ICompanyRepository
    {
        private readonly InterviewContext _context;

        public CompanyRepository(InterviewContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateCompanyAsync(Company company, CancellationToken cancellationToken)
        {
            var result = await _context.AddAsync(company, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<Company?> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return result;
        }

        public async Task<Company?> FindByIsinAsync(string isin, CancellationToken cancellationToken)
        {
            var result = await _context.Companies.FirstOrDefaultAsync(x => x.Isin.Value == isin, cancellationToken);
            return result;
        }

        public IAsyncEnumerable<Company> GetAllCompanies(CancellationToken cancellationToken)
        {
            return _context.Companies.AsAsyncEnumerable();
        }
    }
}