using AutoFixture;
using FluentAssertions;
using Interview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Interview.Infrastructure.Tests
{
    public class CompanyRepositoryTests
    {
        private readonly IFixture _fixture;

        public CompanyRepositoryTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task NotAllowToCreateMultipleCompaniesWithTheSameIsin()
        {
            var builder = new DbContextOptionsBuilder<InterviewContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new InterviewContext(builder.Options);

            var service = new CompanyRepository(context);

            var name = _fixture.Create<string>();
            var exchange = _fixture.Create<string>();
            var ticker = _fixture.Create<string>();
            var isin = new CompanyIsin("US45256BAD38");

            var company1 = new Company(name, exchange, ticker, isin);
            var company2 = new Company(name, exchange, ticker, isin);

            var exception = await Record.ExceptionAsync(async () =>
            {
                await service.CreateCompanyAsync(company1, CancellationToken.None);
                await service.CreateCompanyAsync(company2, CancellationToken.None);
            });

            exception.Should().NotBeNull();
        }
    }
}