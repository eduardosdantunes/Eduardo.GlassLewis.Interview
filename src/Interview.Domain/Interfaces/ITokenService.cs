using Interview.Domain.Entities;

namespace Interview.Domain.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, User user);
    }
}