namespace Interview.Domain.Entities;

public class Company
{
    private Company() { }

    public Company(string name, string exchange, string ticker, CompanyIsin isin)
    {
        Name = name;
        Exchange = exchange;
        Ticker = ticker;
        Isin = isin;
    }

    public int Id { get; private set; }
    public string Name { get; init; } = null!;
    public string Exchange { get; init; } = null!;
    public string Ticker { get; init; } = null!;
    public CompanyIsin Isin { get; private set; } = null!;
    public string? WebSite { get; set; }
}