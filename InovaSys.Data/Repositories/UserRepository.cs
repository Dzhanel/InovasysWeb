using Dapper;
using Inovasys.Data.Dapper;
using Inovasys.Data.Entites;
using Inovasys.Data.Interfaces;

namespace Inovasys.Data.Repositories
{
    public class UserRepository : IUserUserRepository
    {
        protected readonly IDapperContext context;

        public UserRepository(DapperContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                using var conn = context.CreateConnection();

                var query = @"
                    SELECT 
                        u.Id, u.Name, u.Username, u.Password, u.Email, u.Phone, 
                        u.Website, u.Note, u.IsActive, u.CreatedAt,
                        a.Id AS Id, a.Street, a.Suite, a.City, a.Zipcode, 
                        a.Latitude, a.Longitude, a.UserId
                    FROM Users u
                    LEFT JOIN Addresses a ON a.UserId = u.Id";

                var userDict = new Dictionary<int, User>();

                await conn.QueryAsync<User, Address, User>(
                    query,
                    (user, address) =>
                    {
                        if (!userDict.TryGetValue(user.Id, out var userEntry))
                        {
                            userEntry = user;
                            userEntry.Address = address;
                            userDict.Add(user.Id, userEntry);
                        }
                        return userEntry;
                    },
                    splitOn: "Id"
                );

                return userDict.Values;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UserCount()
        {
            try
            {
                using var conn = context.CreateConnection();
                var query = "SELECT COUNT(*) FROM Users";
                return await conn.ExecuteScalarAsync<int>(query);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ReplaceAllAsync(ICollection<User> users)
        {
            if (users == null || users.Count == 0)
            {
                return false;
            }

            using var connection = context.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                await connection.ExecuteAsync(
                    "SET IDENTITY_INSERT Users ON",
                    transaction: transaction
                );
                await connection.ExecuteAsync("DELETE FROM Addresses", transaction: transaction);
                await connection.ExecuteAsync("DBCC CHECKIDENT ('Addresses', RESEED, 0)", transaction: transaction);

                await connection.ExecuteAsync("DELETE FROM Users", transaction: transaction);
                await connection.ExecuteAsync("DBCC CHECKIDENT ('Users', RESEED, 0)", transaction: transaction);

                var insertUserQuery = @"
                    INSERT INTO Users (Id, Name, Username, Password, Email, Phone, Website, Note, IsActive, CreatedAt)
                    VALUES (@Id, @Name, @Username, @Password, @Email, @Phone, @Website, @Note, @IsActive, @CreatedAt)";

                foreach (var user in users)
                {
                    user.CreatedAt = DateTime.UtcNow;
                    await connection.ExecuteAsync(insertUserQuery, new
                    {
                        user.Id,
                        user.Name,
                        user.Username,
                        user.Password,
                        user.Email,
                        Phone = user.Phone!,
                        Website = user.Website!,
                        Note = user.Note!,
                        user.IsActive,
                        user.CreatedAt
                    }, transaction: transaction);
                }
                var insertAddressQuery = @"
                    INSERT INTO Addresses (Street, Suite, City, Zipcode, Latitude, Longitude, UserId)
                    VALUES (@Street, @Suite, @City, @Zipcode, @Latitude, @Longitude, @UserId)";

                foreach (var user in users)
                {
                    await connection.ExecuteAsync(insertAddressQuery, new
                    {
                        user.Address!.Street,
                        Suite = user.Address.Suite!,
                        user.Address.City,
                        Zipcode = user.Address.Zipcode!,
                        user.Address.Latitude,
                        user.Address.Longitude,
                        UserId = user.Id
                    }, transaction: transaction);
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
