using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface ISerieRepository
    {   
        int Insert(IUnitOfWork uow, SerieBO entity);
        int Update(IUnitOfWork uow, SerieBO entity);
        void Delete(IUnitOfWork uow, int pk);
        SerieBO Single(IUnitOfWork uow, int pk);
        List<SerieBO> FindAll(IUnitOfWork uow);
        int MaxPK(IUnitOfWork uow);
    }
}