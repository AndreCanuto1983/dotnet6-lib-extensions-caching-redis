using Application.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> SetUser(UserModel user, CancellationToken cancellationToken);
        Task<UserModel> GetUser(Guid userId, CancellationToken cancellationToken);
        Task UpdateUser(UserModel user, CancellationToken cancellationToken);
        Task DeleteUser(Guid userId, CancellationToken cancellationToken);
    }
}
