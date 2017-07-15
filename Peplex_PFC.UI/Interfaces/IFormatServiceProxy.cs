using System.Collections.Generic;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IFormatServiceProxy
    {
        bool Insert(ProxyContext context, FormatUIO entity);
        bool Update(ProxyContext context, FormatUIO entity);
        bool Delete(ProxyContext context, int pk);
        FormatUIO Single(ProxyContext context, int pk);
        IEnumerable<FormatUIO> FindAll(ProxyContext context);
        int MaxPK(ProxyContext context);
    }
}