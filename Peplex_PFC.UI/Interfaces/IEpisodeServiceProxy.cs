using System.Collections.Generic;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IEpisodeServiceProxy
    {
        bool Insert(EpisodeUIO entity);
        bool Update(EpisodeUIO entity);
        bool Delete(int serieId, int season, int number);
        EpisodeUIO Single(int serieId, int season, int number);
        IEnumerable<EpisodeUIO> FindAll();
        int MaxPK();
    }
}