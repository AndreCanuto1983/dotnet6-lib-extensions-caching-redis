using Application.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task SetUser(UserModel user, CancellationToken cancellationToken);
        Task<UserModel> GetUser(string userId, CancellationToken cancellationToken);
        Task UpdateUser(UserModel user, CancellationToken cancellationToken);
        Task DeleteUser(string userId, CancellationToken cancellationToken);
    }
}
