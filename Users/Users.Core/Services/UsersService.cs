using Users.Core.DTO;
using Users.Core.Entities;
using Users.Core.RepositoryContracts;
using Users.Core.ServiceContracts;

namespace Users.Core.Services;

internal class UsersService(IUsersRepository usersRepository) : IUsersService
{
    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        var user = await usersRepository
            .GetUserByEmailAndPassword(
                loginRequest.Email,
                loginRequest.Password);

        if (user is null)
            return null;

        return new(
            user.UserID,
            user.Email,
            user.PersonName,
            user.Gender,
            "token",
            Success: true);
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        var user = new ApplicationUser()
        {
            PersonName = registerRequest.PersonName,
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            Gender = registerRequest.Gender.ToString()
        };

        var registeredUser = await usersRepository.AddUser(user);

        if (registeredUser is null)
            return null;

        return new(
            registeredUser.UserID,
            registeredUser.Email,
            registeredUser.PersonName,
            registeredUser.Gender,
            "token",
            Success: true);
    }
}
