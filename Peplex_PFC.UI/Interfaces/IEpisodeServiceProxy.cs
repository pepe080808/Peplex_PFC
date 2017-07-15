using System.Collections.Generic;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IEpisodeServiceProxy
    {
        bool Insert(ProxyContext context, EpisodeUIO entity);
        bool Update(ProxyContext context, EpisodeUIO entity);
        bool Delete(ProxyContext context, int serieId, int season, int number);
        EpisodeUIO Single(ProxyContext context, int serieId, int season, int number);
        IEnumerable<EpisodeUIO> FindAll(ProxyContext context);
        int MaxPK(ProxyContext context);
    }
}