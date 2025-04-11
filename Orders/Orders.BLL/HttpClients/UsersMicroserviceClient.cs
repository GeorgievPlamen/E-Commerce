using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Orders.BLL.DTO;

namespace Orders.BLL.HttpClients;

public class UsersMicroserviceClient(
    HttpClient httpClient,
    IDistributedCache cache)
{
    public async Task<UserDTO?> GetUserByUserID(Guid userID)
    {
        var cacheKey = $"product:{userID}";

        var cached = await cache.GetStringAsync(cacheKey);

        if (cached is not null)
        {
            return JsonSerializer.Deserialize<UserDTO>(cached);
        }

        var result = await httpClient.GetAsync($"/api/users/{userID}");

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var user = await result.Content.ReadFromJsonAsync<UserDTO>();

        var userCached = JsonSerializer.Serialize(user);
        var cacheOpt = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(300)).SetSlidingExpiration(TimeSpan.FromSeconds(30));
        await cache.SetStringAsync(cacheKey, userCached, cacheOpt);
        return user;
    }
}