namespace ApiGateway.Cache;

public interface IRolePermissionCache
{
    Task<List<string>?> GetAsync(string roleCode);

    Task SetAsync(string roleCode, List<string> permissionCodes, TimeSpan expiration);
}