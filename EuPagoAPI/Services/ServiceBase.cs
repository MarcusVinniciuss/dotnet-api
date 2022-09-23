using EuPagoAPI.Data;
using EuPagoAPI.Extensions;

namespace EuPagoAPI.Services
{
    public class ServiceBase
    {
        protected DataContext? _dataContext;

        public ServiceBase(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public decimal GetId<T>(string sequenceName)
            where T : class
        {
            var command = _dataContext.Database.GetDbConnection().CreateCommand();

            var tableName = _dataContext.TableName(typeof(T));

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = $"SELECT {sequenceName}.NEXTVAL FROM DUAL";

            _dataContext.Database.OpenConnection();

            try
            {
                var result = (decimal?)command.ExecuteScalar();

                return result.Value;
            }
            finally
            {
                _dataContext.Database.CloseConnection();
            }
        }
    }
}
