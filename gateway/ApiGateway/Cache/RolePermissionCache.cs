using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiGateway.Cache;

public class RolePermissionCache : IRolePermissionCache
{
    private readonly IDistributedCache _distributedCache;

    public RolePermissionCache(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<List<string>?> GetAsync(string roleCode)
    {
        var cacheKey = GetCacheKey(roleCode);
        var cachedData = await _distributedCache.GetStringAsync(cacheKey);

        if (string.IsNullOrEmpty(cachedData))
        {
            return null;
        }

        return JsonSerializer.Deserialize<List<string>>(cachedData);
    }

    public async Task SetAsync(string roleCode, List<string> permissionCodes, TimeSpan expiration)
    {
        var cacheKey = GetCacheKey(roleCode);
        var serializedData = JsonSerializer.Serialize(permissionCodes);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        await _distributedCache.SetStringAsync(cacheKey, serializedData, options);
    }

    private string GetCacheKey(string roleCode)
    {
        return $"RolePermissions:{roleCode}";
    }
}