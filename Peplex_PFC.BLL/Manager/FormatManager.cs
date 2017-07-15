using System;
using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.BLL.Manager
{
    public class FormatManager : IFormatManager
    {
        private readonly IFormatRepository _repository;

        public FormatManager(IFormatRepository repository)
        {
            _repository = repository;
        }

        public void Insert(IUnitOfWork unitOfWork, FormatBO entity)
        {
            _repository.Insert(unitOfWork, entity);
        }

        public void Update(IUnitOfWork unitOfWork, FormatBO entity)
        {
            _repository.Update(unitOfWork, entity);
        }

        public void Delete(IUnitOfWork unitOfWork, int pk)
        {
            _repository.Delete(unitOfWork, pk);
        }

        public FormatBO Single(IUnitOfWork unitOfWork, int pk)
        {
            return _repository.Single(unitOfWork, pk);
        }

        public List<FormatBO> FindAll(IUnitOfWork unitOfWork)
        {
            return _repository.FindAll(unitOfWork);
        }

        public int MaxPK(IUnitOfWork unitOfWork)
        {
            return _repository.MaxPK(unitOfWork);
        }
    }
}