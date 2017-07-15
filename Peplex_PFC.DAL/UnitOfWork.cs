using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public string ConnectionString { get; set; }

        private readonly SqlConnection _sqlConnection;
        private readonly SqlTransaction _sqlTransaction;

        public UnitOfWork(string connectionString)
        {
            ConnectionString = connectionString;

            _sqlConnection = new SqlConnection(connectionString);
                _sqlConnection.Open();

            _sqlTransaction = _sqlConnection.BeginTransaction();
        }

        public DbCommand CreateCommand(string cmdText, DbParameter[] parameters)
        {
            var cmd = new SqlCommand(cmdText, _sqlConnection, _sqlTransaction);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        public void Commit()
        {
            _sqlTransaction.Commit();
        }

        public void Dispose()
        {
            _sqlConnection.Close();
        }
    }
}
