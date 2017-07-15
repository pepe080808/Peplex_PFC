using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IUserRepository
    {
        void Insert(IUnitOfWork uow, UserBO entity);
        void Update(IUnitOfWork uow, UserBO entity);
        void Delete(IUnitOfWork uow, int pk);
        UserBO Single(IUnitOfWork uow, int pk);
        List<UserBO> FindAll(IUnitOfWork uow);
        int MaxPK(IUnitOfWork uow);
    }
}