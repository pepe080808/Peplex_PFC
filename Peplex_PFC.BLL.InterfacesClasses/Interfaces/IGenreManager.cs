using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IGenreManager
    {
        void Insert(IUnitOfWork unitOfWork, GenreBO entity);
        void Update(IUnitOfWork unitOfWork, GenreBO entity);
        void Delete(IUnitOfWork unitOfWork, int pk);
        GenreBO Single(IUnitOfWork unitOfWork, int pk);
        List<GenreBO> FindAll(IUnitOfWork unitOfWork);
        int MaxPK(IUnitOfWork unitOfWork);
    }
}
