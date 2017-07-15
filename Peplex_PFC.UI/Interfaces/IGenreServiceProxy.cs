using System.Collections.Generic;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IGenreServiceProxy
    {
        bool Insert(ProxyContext context, GenreUIO entity);
        bool Update(ProxyContext context, GenreUIO entity);
        bool Delete(ProxyContext context, int pk);
        GenreUIO Single(ProxyContext context, int pk);
        IEnumerable<GenreUIO> FindAll(ProxyContext context);
        int MaxPK(ProxyContext context);
    }
}
