using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.BLL.Manager
{
    public class GenreManager : IGenreManager
    {
        private readonly IGenreRepository _repository;

        public GenreManager(IGenreRepository repository)
        {
            _repository = repository;
        }

        public void Insert(IUnitOfWork unitOfWork, GenreBO entity)
        {
            _repository.Insert(unitOfWork, entity);
        }

        public void Update(IUnitOfWork unitOfWork, GenreBO entity)
        {
            _repository.Update(unitOfWork, entity);
        }

        public void Delete(IUnitOfWork unitOfWork, int pk)
        {
            _repository.Delete(unitOfWork, pk);
        }

        public GenreBO Single(IUnitOfWork unitOfWork, int pk)
        {
            return _repository.Single(unitOfWork, pk);
        }

        public List<GenreBO> FindAll(IUnitOfWork unitOfWork)
        {
            return _repository.FindAll(unitOfWork);
        }

        public int MaxPK(IUnitOfWork unitOfWork)
        {
            return _repository.MaxPK(unitOfWork);
        }
    }
}
