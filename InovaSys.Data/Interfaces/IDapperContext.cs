using System.Data;

namespace Inovasys.Data.Interfaces
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
