using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Peplex_PFC.DAL
{
    public static class ExtensionsMethods
    {
        public static void AddWithValue(this DbParameterCollection parameters, string parameterName, object parameterValue)
        {
            parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Value = parameterValue
            });
        }

        public static DbParameter Add(this DbParameterCollection parameters, string parameterName, SqlDbType paramterType)
        {
            var p = new SqlParameter(parameterName, paramterType);
            parameters.Add(p);
            return p;
        }

        public static DbParameter Add(this DbParameterCollection parameters, string parameterName, SqlDbType paramterType, int size)
        {
            var p = new SqlParameter(parameterName, paramterType, size);
            parameters.Add(p);
            return p;
        }
    }
}
