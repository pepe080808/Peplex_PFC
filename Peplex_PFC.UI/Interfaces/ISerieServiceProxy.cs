using System.Collections.Generic;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface ISerieServiceProxy
    {
        bool Insert(ProxyContext context, SerieUIO entity);
        bool Update(ProxyContext context, SerieUIO entity);
        bool Delete(ProxyContext context, int pk);
        SerieUIO Single(ProxyContext context, int pk);
        IEnumerable<SerieUIO> FindAll(ProxyContext context);
        int MaxPK(ProxyContext context);
    }
}