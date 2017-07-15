using System.Collections.Generic;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IUserServiceProxy
    {
        bool Insert(ProxyContext context, UserUIO entity);
        bool Update(ProxyContext context, UserUIO entity);
        bool Delete(ProxyContext context, int pk);
        UserUIO Single(ProxyContext context, int pk);
        IEnumerable<UserUIO> FindAll(ProxyContext context);
        int MaxPK(ProxyContext context);
    }
}