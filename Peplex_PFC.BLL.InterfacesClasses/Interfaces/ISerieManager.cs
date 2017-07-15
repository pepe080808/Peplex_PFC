using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface ISerieManager
    {
        int Insert(IUnitOfWork unitOfWork, SerieBO entity);
        int Update(IUnitOfWork unitOfWork, SerieBO entity);
        void Delete(IUnitOfWork unitOfWork, int pk);
        SerieBO Single(IUnitOfWork unitOfWork, int pk);
        List<SerieBO> FindAll(IUnitOfWork unitOfWork);
        int MaxPK(IUnitOfWork unitOfWork);
    }
}