using System.Text.RegularExpressions;

namespace Interview.Domain.Entities;

public record CompanyIsin
{
    public CompanyIsin(string value)
    {
        SetIsin(value);
    }

    public string Value { get; private set; } = string.Empty;

    public void SetIsin(string value)
    {
        var match = Regex.Match(value, "([A-Z]{2})([A-Z0-9]{9})([0-9]{1})");
        if (!match.Success) throw new Exception("invalid isin");
        Value = value;
    }
}