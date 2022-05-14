namespace Interview.Domain.Entities;

public class Company
{
    private Company() { }

    public Company(string name, string exchange, string ticker, CompanyIsin isin, string? webSite = null)
    {
        SetName(name);
        SetExchange(exchange);
        SetTicker(ticker);
        this.Isin = isin;
        this.WebSite = webSite;
    }

    public int Id { get; private set; }
    public string Name { get; set; } 
    public string Exchange { get; set; }
    public string Ticker { get; set; }
    public CompanyIsin Isin { get; set; } = null!;
    public string? WebSite { get; set; }

    public void SetName(string Name)
    {
        if (string.IsNullOrEmpty(Name) || Name.Trim() == "")
        {
            throw new ArgumentNullException("Name");
        }
        this.Name = Name;
    }

    public void SetExchange(string Exchange)
    {
        if (string.IsNullOrEmpty(Exchange) || Exchange.Trim() == "")
        {
            throw new ArgumentNullException("Exchange");
        }
        this.Exchange = Exchange;
    }

    public void SetTicker(string Ticker)
    {
        if (string.IsNullOrEmpty(Ticker) || Ticker.Trim() == "")
        {
            throw new ArgumentNullException("Ticker");
        }
        this.Ticker = Ticker;
    }
}