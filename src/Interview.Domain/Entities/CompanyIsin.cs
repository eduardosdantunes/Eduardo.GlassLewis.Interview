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
        var match = IsinHelper.IsIsin(value);
        if (!match) throw new Exception("invalid isin");
        Value = value;
    }
}

public static class IsinHelper
{
    private static readonly Regex Pattern = new Regex("[A-Z]{2}([A-Z0-9]){10}", RegexOptions.Compiled);

    public static bool IsIsin(this string isin)
    {
        if (string.IsNullOrEmpty(isin))
        {
            return false;
        }
        if (!Pattern.IsMatch(isin))
        {
            return false;
        }

        var digits = new int[22];
        int index = 0;
        for (int i = 0; i < 11; i++)
        {
            char c = isin[i];
            if (c >= '0' && c <= '9')
            {
                digits[index++] = c - '0';
            }
            else if (c >= 'A' && c <= 'Z')
            {
                int n = c - 'A' + 10;
                int tens = n / 10;
                if (tens != 0)
                {
                    digits[index++] = tens;
                }
                digits[index++] = n % 10;
            }
            else
            {
                return false;
            }
        }
        int sum = 0;
        for (int i = 0; i < index; i++)
        {
            int digit = digits[index - 1 - i];
            if (i % 2 == 0)
            {
                digit *= 2;
            }
            sum += digit / 10;
            sum += digit % 10;
        }

        int checkDigit = isin[11] - '0';
        if (checkDigit < 0 || checkDigit > 9)
        {
            return false;
        }
        int tensComplement = (sum % 10 == 0) ? 0 : ((sum / 10) + 1) * 10 - sum;
        return checkDigit == tensComplement;
    }
}