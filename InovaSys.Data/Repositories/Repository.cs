using Dapper;
using Inovasys.Data.Interfaces;
using InovaSys.Data.Dapper;
using InovaSys.Data.Interfaces;

namespace InovaSys.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDapperContext context;
        protected readonly string tableName;

        public Repository(DapperContext _context, string _tableName)
        {
            context = _context;
            tableName = _tableName;
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var conn = context.CreateConnection();
            var query = $"SELECT * FROM [{tableName}]";
            return await conn.QueryAsync<T>(query);
        }

        public Task<T> GetByApiId()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
