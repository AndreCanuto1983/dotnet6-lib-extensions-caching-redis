using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<IUserRepository> _logger;

        public UserRepository(
            IDistributedCache distributedCache,
            ILogger<IUserRepository> logger
            )
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task<UserModel> SetUser(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(user.Id.ToString(), cancellationToken);

                if (userObject != null)
                    throw new InvalidOperationException("Existing user");                    

                string userJson = JsonSerializer.Serialize(user);

                await _distributedCache.SetStringAsync(user.Id.ToString(), userJson, cancellationToken);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[UserRepository][SetUser]");
                throw;
            }
        }

        public async Task<UserModel?> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(userId.ToString(), cancellationToken);

                if (userObject == null)
                    return null;

                return JsonSerializer.Deserialize<UserModel>(userObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[UserRepository][GetUser]");
                throw;
            }
        }

        public async Task UpdateUser(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(user.Id.ToString(), cancellationToken);

                if (userObject != null)
                {
                    string userJson = JsonSerializer.Serialize(user);

                    await _distributedCache.SetStringAsync(user.Id.ToString(), userJson, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[UserRepository][UpdateUser]");
                throw;
            }
        }

        public async Task DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                await _distributedCache.RemoveAsync(userId.ToString(), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[UserRepository][DeleteUser]");
                throw;
            }
        }
    }
}