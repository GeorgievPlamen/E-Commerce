using Dapper;
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
            ("UserID", "Email", "PersonName","Gender","Password") 
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
        var query = """
            SELECT * FROM public."Users" 
            WHERE "Email"=@Email AND "Password"=@Password
            """;

        var user = await dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, new { Email = email, Password = password });

        return user;
    }

    public async Task<ApplicationUser?> GetUserByUserID(Guid? userID)
    {
        var query = """
            SELECT * FROM public."Users"
            WHERE "UserID"=@UserID
            """;

        var parameters = new { UserID = userID };

        using var connection = dbContext.DbConnection;

        return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);
    }
}
