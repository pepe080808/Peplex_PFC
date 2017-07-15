using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IFilmRepository
    {
        void Insert(IUnitOfWork uow, FilmBO entity);
        void Update(IUnitOfWork uow, FilmBO entity);
        void Delete(IUnitOfWork uow, int pk);
        FilmBO Single(IUnitOfWork uow, int pk);
        List<FilmBO> FindAll(IUnitOfWork uow);
        int MaxPK(IUnitOfWork uow);
    }
}