using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IEpisodeRepository
    {
        void Insert(IUnitOfWork uow, EpisodeBO entity);
        void Update(IUnitOfWork uow, EpisodeBO entity);
        void Delete(IUnitOfWork uow, int serieId, int season, int number);
        EpisodeBO Single(IUnitOfWork uow, int serieId, int season, int number);
        List<EpisodeBO> FindAll(IUnitOfWork uow);
        int MaxPK(IUnitOfWork uow);
    }
}