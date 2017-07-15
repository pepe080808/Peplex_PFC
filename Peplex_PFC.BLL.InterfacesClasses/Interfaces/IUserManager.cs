using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IUserManager
    {
        void Insert(IUnitOfWork unitOfWork, UserBO entity);
        void Update(IUnitOfWork unitOfWork, UserBO entity);
        void Delete(IUnitOfWork unitOfWork, int pk);
        UserBO Single(IUnitOfWork unitOfWork, int pk);
        List<UserBO> FindAll(IUnitOfWork unitOfWork);
        int MaxPK(IUnitOfWork unitOfWork);
    }
}