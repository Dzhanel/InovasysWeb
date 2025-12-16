using Inovasys.Data.Entites;

namespace Inovasys.Data.Interfaces
{
    public interface IApiUserRepository
    {
        Task<List<User>> FetchUsersAsync();   
    }
}
