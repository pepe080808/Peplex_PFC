using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IFormatRepository
    {
        void Insert(IUnitOfWork uow, FormatBO entity);
        void Update(IUnitOfWork uow, FormatBO entity);
        void Delete(IUnitOfWork uow, int pk);
        FormatBO Single(IUnitOfWork uow, int pk);
        List<FormatBO> FindAll(IUnitOfWork uow);
        int MaxPK(IUnitOfWork uow);
    }
}