using Interview.Domain.Entities;
using Interview.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Interview.Infrastructure
{
    internal class UserRepository : IUserRepository
    {
        private readonly InterviewContext _context;

        public UserRepository(InterviewContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            var result = await _context.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<User?> GetUserAsync(string userName, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
            return result;
        }
        public async Task<User?> ValidateLoginAsync(User user, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => string.Equals(x.UserName, user.UserName) && string.Equals(x.Password, user.Password));
            return result;
        }

        public async Task<User?> ChangePasswordAsync(string userName, User user, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);

            if (result != null)
            {
                if (user.Password != null)
                    result.Password = user.Password;

                _context.Users.Update(result);
                await _context.SaveChangesAsync(cancellationToken);

                return result;
            }

            return result;
        }
    }
}