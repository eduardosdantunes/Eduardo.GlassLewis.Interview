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
    public void Teste()
    {
        var name = _fixture.Create<string>();
        var exchange = _fixture.Create<string>();
        var ticker = _fixture.Create<string>();
        var isin = new CompanyIsin("US45256BAD38");

        var company = new Company(name, exchange, ticker, isin);
    }
}

public class CompanyIsinTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("BR")]
    [InlineData("Eduardo Antunes")]
    [InlineData("123456791011")]
    [InlineData("ABCDEFDFGHIJ")]
    [InlineData("B71234567891")]
    [InlineData("771234567891")]
    public void ThrowExceptionWhenIsinIsInvalid(string isinInput)
    {
        // Arrange
        var task = Task.Run(() =>
        {
            var isin = new CompanyIsin(isinInput);
        });

        // Act
        var taskException = Record.ExceptionAsync(async () => await task);

        // Assert
        taskException.Should().NotBeNull();
        taskException.Result.Should().NotBeNull();
        taskException.Result.Message.Should().Contain("invalid isin");
    }

    [Fact]
    public void ThrowExceptionWhenIsinIsNull()
    {
        // Arrange
        var task = Task.Run(() =>
        {
            var isin = new CompanyIsin(null);
        });

        // Act
        var taskException = Record.ExceptionAsync(async () => await task);


        // Assert
        taskException.Result.Should().NotBeNull();

    }

    [Theory]
    [InlineData("US45256BAD38")]
    [InlineData("BR1234567891")]
    public void SetIsinIsValid(string isinInput)
    {
        // Arrange
        var isin = new CompanyIsin(isinInput);

        // Act
        var exception = Record.Exception(() => isin.SetIsin(isinInput));

        // Assert
        exception.Should().BeNull();
    }
}