using Inovasys.Data.Entites;

namespace Inovasys.Data.Interfaces
{
    public interface IUserUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> ReplaceAllAsync(ICollection<User> users);
        Task<int> UserCount();
    }
}
