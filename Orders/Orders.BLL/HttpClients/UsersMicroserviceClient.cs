using System.Net.Http.Json;
using Orders.BLL.DTO;

namespace Orders.BLL.HttpClients;

public class UsersMicroserviceClient(HttpClient httpClient)
{
    public async Task<UserDTO?> GetUserByUserID(Guid userID)
    {
        var result = await httpClient.GetAsync($"/api/users/{userID}");

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var user = await result.Content.ReadFromJsonAsync<UserDTO>();

        return user;
    }
}