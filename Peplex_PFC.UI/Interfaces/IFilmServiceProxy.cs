using System.Collections.Generic;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IFilmServiceProxy
    {
        bool Insert(ProxyContext context, FilmUIO entity);
        bool Update(ProxyContext context, FilmUIO entity);
        bool Delete(ProxyContext context, int pk);
        FilmUIO Single(ProxyContext context, int pk);
        IEnumerable<FilmUIO> FindAll(ProxyContext context);
        int MaxPK(ProxyContext context);
    }
}