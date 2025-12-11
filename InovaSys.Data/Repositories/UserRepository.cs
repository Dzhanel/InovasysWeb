using Inovasys.Data.Interfaces;
using InovaSys.Data.Dapper;
using InovaSys.Data.Entites;
using InovaSys.Data.Repositories;

namespace Inovasys.Data.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(DapperContext context)
       : base(context, "Users")
        {
        }
    }
}
