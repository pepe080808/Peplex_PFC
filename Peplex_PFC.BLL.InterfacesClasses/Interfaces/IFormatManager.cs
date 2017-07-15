using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IFormatManager
    {
        void Insert(IUnitOfWork unitOfWork, FormatBO entity);
        void Update(IUnitOfWork unitOfWork, FormatBO entity);
        void Delete(IUnitOfWork unitOfWork, int pk);
        FormatBO Single(IUnitOfWork unitOfWork, int pk);
        List<FormatBO> FindAll(IUnitOfWork unitOfWork);
        int MaxPK(IUnitOfWork unitOfWork);
    }
}