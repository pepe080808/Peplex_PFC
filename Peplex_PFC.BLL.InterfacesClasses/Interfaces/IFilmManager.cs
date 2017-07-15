using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IFilmManager
    {
        void Insert(IUnitOfWork unitOfWork, FilmBO entity);
        void Update(IUnitOfWork unitOfWork, FilmBO entity);
        void Delete(IUnitOfWork unitOfWork, int pk);
        FilmBO Single(IUnitOfWork unitOfWork, int pk);
        List<FilmBO> FindAll(IUnitOfWork unitOfWork);
        int MaxPK(IUnitOfWork unitOfWork);
    }
}