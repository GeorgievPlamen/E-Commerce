using Users.Core.DTO;
using Users.Core.Entities;
using Users.Core.RepositoryContracts;

namespace Users.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        user.UserID = Guid.CreateVersion7();

        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        return new()
        {
            UserID = Guid.CreateVersion7(),
            Email = email,
            Password = password,
            PersonName = "Fake name",
            Gender = GenderOptions.Male.ToString()
        };
    }
}
