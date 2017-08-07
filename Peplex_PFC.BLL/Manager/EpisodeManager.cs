using System.Collections.Generic;
using System.Linq;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.BLL.Manager
{
    public class EpisodeManager : IEpisodeManager
    {
        private readonly IEpisodeRepository _repository;
        private readonly IFormatManager _formatManager;

        public EpisodeManager(IEpisodeRepository repository, IFormatManager formatManager)
        {
            _repository = repository;
            _formatManager = formatManager;
        }

        public void Insert(IUnitOfWork unitOfWork, EpisodeBO entity)
        {
            _repository.Insert(unitOfWork, entity);
        }

        public void Update(IUnitOfWork unitOfWork, EpisodeBO entity)
        {
            _repository.Update(unitOfWork, entity);
        }

        public void Delete(IUnitOfWork unitOfWork, int serieId, int season, int number)
        {
            _repository.Delete(unitOfWork, serieId, season, number);
        }

        public EpisodeBO Single(IUnitOfWork unitOfWork, int serieId, int season, int number)
        {
            var result = _repository.Single(unitOfWork, serieId, season, number);
            var formats = _formatManager.FindAll(unitOfWork);

            result.FormatName = formats.First(f => f.Id == result.FormatId).Name;
            result.QualityName = formats.First(f => f.Id == result.FormatId).Quality;

            return result;
        }

        public List<EpisodeBO> FindAll(IUnitOfWork unitOfWork)
        {
            var result = _repository.FindAll(unitOfWork);
            var formats = _formatManager.FindAll(unitOfWork);

            foreach (var r in result)
            {
                r.FormatName = formats.First(f => f.Id == r.FormatId).Name;
                r.QualityName = formats.First(f => f.Id == r.FormatId).Quality;
            }

            return result;
        }

        public int MaxPK(IUnitOfWork unitOfWork)
        {
            return _repository.MaxPK(unitOfWork);
        }
    }
}