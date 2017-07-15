using System;
using System.Data.Common;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        string ConnectionString { get; }
        DbCommand CreateCommand(string cmdText, DbParameter[] parameters = null);
        void Commit();
    }
}
