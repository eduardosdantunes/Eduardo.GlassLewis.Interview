namespace Interview.Domain.Entities;

public class Company
{
    private Company() { }

    public Company(string name, string exchange, string ticker, CompanyIsin isin, string? webSite = null)
    {
        Name = name;
        Exchange = exchange;
        Ticker = ticker;
        Isin = isin;
        WebSite = webSite;
    }

    public int Id { get; private set; }
    public string Name { get; set; }
    public string Exchange { get; set; }
    public string Ticker { get; set; }
    public CompanyIsin Isin { get; set; } = null!;
    public string? WebSite { get; set; }
}