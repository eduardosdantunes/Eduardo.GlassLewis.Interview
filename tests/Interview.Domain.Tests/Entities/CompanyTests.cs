using AutoFixture;
using FluentAssertions;
using Interview.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace Interview.Domain.Tests.Entities;

public class CompanyTests
{
    private readonly IFixture _fixture;

    public CompanyTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void CompanyTypeTest()
    {
        var name = _fixture.Create<string>();
        var exchange = _fixture.Create<string>();
        var ticker = _fixture.Create<string>();
        var isin = new CompanyIsin("US45256BAD38");
        var webSite = _fixture.Create<string>();

        var company = new Company(name, exchange, ticker, isin, webSite);

        company.Should().Be(company);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetInvalidName(string nameInput)
    {
        // Arrange
        var isin = new CompanyIsin("US45256BAD38");
        var exchange = _fixture.Create<string>();
        var ticker = _fixture.Create<string>();

        // Act
        var exception = Record.Exception(() => new Company(nameInput, exchange, ticker, isin));

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().Contain("Value cannot be null");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetInvalidExchange(string exchangeInput)
    {
        // Arrange
        var isin = new CompanyIsin("US45256BAD38");
        var name = _fixture.Create<string>();
        var ticker = _fixture.Create<string>();

        // Act
        var exception = Record.Exception(() => new Company(name, exchangeInput, ticker, isin));

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().Contain("Value cannot be null");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetInvalidTicket(string tickerInput)
    {
        // Arrange
        var isin = new CompanyIsin("US45256BAD38");
        var name = _fixture.Create<string>();
        var exchange = _fixture.Create<string>();

        // Act
        var exception = Record.Exception(() => new Company(name, exchange, tickerInput, isin));

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().Contain("Value cannot be null");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("ANBA&$&@NSDS*!!)@*&$¨@,~;/;")]
    [InlineData("https://github.com/eduardosdantunes/Eduardo.GlassLewis.Interview")]
    [InlineData("http://www.apple.com")]
    public void SetValidWebSite(string webSiteInput)
    {
        // Arrange
        var isin = new CompanyIsin("US45256BAD38");

        // Act
        var exception = Record.Exception(() => new Company("name", "exchange", "ticket", isin, webSiteInput));

        // Assert
        exception.Should().BeNull();
    }
}