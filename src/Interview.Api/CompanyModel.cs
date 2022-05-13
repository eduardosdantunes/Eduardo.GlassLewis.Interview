namespace Interview.Api;

public class CompanyModel
{
    public string Name { get; set; } = null!;
    public string Exchange { get; set; } = null!;
    public string Ticker { get; set; } = null!;
    public string Isin { get; set; } = null!;
    public string? WebSite { get; set; }
}