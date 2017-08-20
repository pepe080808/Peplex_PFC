using System.Collections.Generic;
using System.Linq;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.BLL.Manager
{
    public class SerieManager : ISerieManager
    {
        private readonly ISerieRepository _repository;
        private readonly IGenreManager _genreManager;
        private readonly IEpisodeManager _episodeManager;

        public SerieManager(ISerieRepository repository, IGenreManager genreManager, IEpisodeManager episodeManager)
        {
            _repository = repository;
            _genreManager = genreManager;
            _episodeManager = episodeManager;
        }

        public int Insert(IUnitOfWork unitOfWork, SerieBO entity)
        {
            return _repository.Insert(unitOfWork, entity);
        }

        public int  Update(IUnitOfWork unitOfWork, SerieBO entity)
        {
            return _repository.Update(unitOfWork, entity);
        }

        public void Delete(IUnitOfWork unitOfWork, int pk)
        {
            _repository.Delete(unitOfWork, pk);
        }

        public SerieBO Single(IUnitOfWork unitOfWork, int pk)
        {
            var result = _repository.Single(unitOfWork, pk);
            var genres = _genreManager.FindAll(unitOfWork);
            var episodes = _episodeManager.FindAll(unitOfWork);

            result.GenreName01 = genres.First(g => g.Id == result.GenreId01).Name;
            result.GenreName02 = genres.First(g => g.Id == result.GenreId02).Name;

            episodes.Where(e => e.SerieId == result.Id).ToList().ForEach(e => result.Episodes.Add(e));
                
            return result;
        }

        public List<SerieBO> FindAll(IUnitOfWork unitOfWork)
        {
            var result = _repository.FindAll(unitOfWork);
            var genres = _genreManager.FindAll(unitOfWork);

            foreach (var r in result)
            {
                r.GenreName01 = genres.First(g => g.Id == r.GenreId01).Name;
                r.GenreName02 = genres.First(g => g.Id == r.GenreId02).Name;
            }

            return result;
        }

        public int MaxPK(IUnitOfWork unitOfWork)
        {
            return _repository.MaxPK(unitOfWork);
        }
    }
}