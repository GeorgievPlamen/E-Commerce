using AutoMapper;
using Microsoft.Graph;
using Users.Core.DTO;
using Users.Core.Entities;
using Users.Core.RepositoryContracts;
using Users.Core.ServiceContracts;

namespace Users.Core.Services;

internal class UsersService(
    IUsersRepository usersRepository,
    GraphServiceClient graphServiceClient,
    IMapper mapper) : IUsersService
{
    public async Task<UserDTO?> GetUserByUserID(Guid userID)
    {
        var existingUser = await graphServiceClient.Users[Convert.ToString(userID)].GetAsync();

        if (existingUser is null) return null;

        var user = new UserDTO(Guid.Parse(existingUser.Id!), existingUser.UserPrincipalName, existingUser.GivenName, existingUser.Surname);

        return user;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        var user = await usersRepository
            .GetUserByEmailAndPassword(
                loginRequest.Email,
                loginRequest.Password);

        if (user is null)
            return null;

        return mapper.Map<AuthenticationResponse>(user) with { Success = true, Token = "token" };
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        var user = mapper.Map<ApplicationUser>(registerRequest);

        var registeredUser = await usersRepository.AddUser(user);

        if (registeredUser is null)
            return null;

        var response = mapper.Map<AuthenticationResponse>(registeredUser) with { Success = true, Token = "token" };

        return response;
    }
}
