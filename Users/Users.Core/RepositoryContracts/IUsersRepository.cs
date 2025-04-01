using Users.Core.Entities;

namespace Users.Core.RepositoryContracts;

public interface IUsersRepository
{
    Task<ApplicationUser?> AddUser(ApplicationUser user);

    Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password);
}