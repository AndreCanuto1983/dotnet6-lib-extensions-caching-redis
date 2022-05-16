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
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            IDistributedCache distributedCache,
            ILogger<UserRepository> logger
            )
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task SetUser(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(user.Id, cancellationToken);

                if (userObject != null)
                    throw new InvalidOperationException("Existing user");

                await _distributedCache.SetStringAsync(
                    user.Id,
                    JsonSerializer.Serialize(user),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][SetUser] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task<UserModel?> GetUser(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(userId, cancellationToken);

                if (userObject == null)
                    return null;

                return JsonSerializer.Deserialize<UserModel>(userObject);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][GetUser] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateUser(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(user.Id, cancellationToken);

                if (userObject != null)
                    await _distributedCache.SetStringAsync(
                        user.Id.ToString(),
                        JsonSerializer.Serialize(user),
                        cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][UpdateUser] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteUser(string userId, CancellationToken cancellationToken)
        {
            try
            {
                await _distributedCache.RemoveAsync(userId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][DeleteUser] => EXCEPTION: {ex.Message}");
                throw;
            }
        }
    }
}