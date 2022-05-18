using Interview.Domain.Entities;

namespace Interview.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetUserAsync(string userName, CancellationToken cancellation);
        Task<User?> ValidateLoginAsync(User user, CancellationToken cancellation);
        Task<User?> ChangePasswordAsync(string userName, User user, CancellationToken cancellationToken);
    }
}