using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;

namespace Peplex_PFC.BLL.InterfacesClasses.Interfaces
{
    public interface IGenreRepository
    {
        void Insert(IUnitOfWork uow, GenreBO entity);
        void Update(IUnitOfWork uow, GenreBO entity);
        void Delete(IUnitOfWork uow, int pk);
        GenreBO Single(IUnitOfWork uow, int pk);
        List<GenreBO> FindAll(IUnitOfWork uow);
        int MaxPK(IUnitOfWork uow);
    }
}
