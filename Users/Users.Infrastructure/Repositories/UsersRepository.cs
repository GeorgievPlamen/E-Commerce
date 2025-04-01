using Dapper;
using Users.Core.DTO;
using Users.Core.Entities;
using Users.Core.RepositoryContracts;
using Users.Infrastructure.DbContext;

namespace Users.Infrastructure.Repositories;

public class UsersRepository(DapperDbContext dbContext) : IUsersRepository
{
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        user.UserID = Guid.CreateVersion7();

        string query = """
            INSERT INTO public."Users"
            ("UserID", ""Email", "PersonName","Gender","Password") 
            VALUES
            (@UserID, @Email, @PersonName, @Gender, @Password)
            """;

        var rowsAffected = await dbContext.DbConnection.ExecuteAsync(query, user);

        if (rowsAffected == 0)
            return null;

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
