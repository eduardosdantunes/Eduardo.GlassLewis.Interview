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
        public async Task ValidatingGetCompany()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<InterviewContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new InterviewContext(builder.Options);

            var service = new CompanyRepository(context);

            var name = _fixture.Create<string>();
            var exchange = _fixture.Create<string>();
            var ticker = _fixture.Create<string>();
            var isin = new CompanyIsin("US45256BAD38");

            var company = new Company(name, exchange, ticker, isin);

            // Act
            await service.CreateCompanyAsync(company, CancellationToken.None);
            var result = await service.FindByIdAsync(1, CancellationToken.None);

            //Assert
            result.Should().Be(company);

        }

        [Fact]
        public async Task ValidatingPostCompany()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<InterviewContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new InterviewContext(builder.Options);

            var service = new CompanyRepository(context);

            var name = _fixture.Create<string>();
            var exchange = _fixture.Create<string>();
            var ticker = _fixture.Create<string>();
            var isin = new CompanyIsin("US45256BAD38");

            var company = new Company(name, exchange, ticker, isin);

            // Act
            var exception = await Record.ExceptionAsync(async () =>
            {
                await service.CreateCompanyAsync(company, CancellationToken.None);
            });

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public async Task NotAllowToCreateMultipleCompaniesWithTheSameId()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<InterviewContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new InterviewContext(builder.Options);

            var service = new CompanyRepository(context);

            var name = _fixture.Create<string>();
            var exchange = _fixture.Create<string>();
            var ticker = _fixture.Create<string>();
            var isin = new CompanyIsin("US45256BAD38");

            var company = new Company(name, exchange, ticker, isin);

            // Act
            var exception = await Record.ExceptionAsync(async () =>
            {
                await service.CreateCompanyAsync(company, CancellationToken.None);
                await service.CreateCompanyAsync(company, CancellationToken.None);
            });

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Contain("An item with the same key has already been added");
        }

        [Fact]
        public async Task NotAllowToCreateMultipleCompaniesWithTheSameIsin()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<InterviewContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new InterviewContext(builder.Options);

            var service = new CompanyRepository(context);

            var name = _fixture.Create<string>();
            var exchange = _fixture.Create<string>();
            var ticker = _fixture.Create<string>();
            var isin = new CompanyIsin("US45256BAD38");

            var company1 = new Company(name, exchange, ticker, isin);
            var company2 = new Company(name, exchange, ticker, isin);


            await service.CreateCompanyAsync(company1, CancellationToken.None);

            // Act
            var exception = await Record.ExceptionAsync(async () =>            {
                
                await service.CreateCompanyAsync(company2, CancellationToken.None);
            });

            //Assert
            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task NotAllowToModifyExistingCompaniesWithTheSameIsin()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<InterviewContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new InterviewContext(builder.Options);

            var service = new CompanyRepository(context);

            var name = _fixture.Create<string>();
            var exchange = _fixture.Create<string>();
            var ticker = _fixture.Create<string>();
            var isin1 = new CompanyIsin("US0378331005");
            var isin2 = new CompanyIsin("US1104193065");            

            var company1 = new Company(name, exchange, ticker, isin1);
            var company2 = new Company(name, exchange, ticker, isin2);
            var company3 = new Company(name, exchange, ticker, isin1);

            await service.CreateCompanyAsync(company1, CancellationToken.None);
            await service.CreateCompanyAsync(company2, CancellationToken.None);

            // Act
            var exception = await Record.ExceptionAsync(async () => {

                await service.SaveChangesAsync(2,company3, CancellationToken.None);
            });

            //Assert
            exception.Should().NotBeNull();
        }
    }
}