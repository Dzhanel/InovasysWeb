using System.Data;

namespace InovaSys.Data.Interfaces
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
