using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IEpisodeManager
    {
        void Insert(IUnitOfWork unitOfWork, EpisodeBO entity);
        void Update(IUnitOfWork unitOfWork, EpisodeBO entity);
        void Delete(IUnitOfWork unitOfWork, int serieId, int season, int number);
        EpisodeBO Single(IUnitOfWork unitOfWork, int serieId, int season, int number);
        List<EpisodeBO> FindAll(IUnitOfWork unitOfWork);
        int MaxPK(IUnitOfWork unitOfWork);
    }
}