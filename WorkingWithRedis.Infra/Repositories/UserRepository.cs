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
                    return null;

                string userJson = JsonSerializer.Serialize(user);

                await _distributedCache.SetStringAsync(user.Id.ToString(), userJson, cancellationToken);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[UserRepository][SetUser]");
                return null;
            }
        }

        public async Task<UserModel> GetUser(Guid userId, CancellationToken cancellationToken)
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
                return null;
            }
        }

        public async Task<bool> UpdateUser(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(user.Id.ToString(), cancellationToken);

                if (userObject == null)
                    return false;

                string userJson = JsonSerializer.Serialize(user);

                await _distributedCache.SetStringAsync(user.Id.ToString(), userJson, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[UserRepository][UpdateUser]");
                return false;
            }
        }

        public async Task<bool> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(userId.ToString(), cancellationToken);

                if (userObject == null)
                    return false;

                await _distributedCache.RemoveAsync(userId.ToString(), cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[UserRepository][DeleteUser]");
                return false;
            }
        }
    }
}