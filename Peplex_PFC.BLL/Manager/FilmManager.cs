using System.Collections.Generic;
using System.Linq;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.BLL.Manager
{
    public class FilmManager : IFilmManager
    {
        private readonly IFilmRepository _repository;
        private readonly IFormatManager _formatManager;
        private readonly IGenreManager _genreManager;

        public FilmManager(IFilmRepository repository, IFormatManager formatManager, IGenreManager genreManager)
        {
            _repository = repository;
            _formatManager = formatManager;
            _genreManager = genreManager;
        }

        public void Insert(IUnitOfWork unitOfWork, FilmBO entity)
        {
            _repository.Insert(unitOfWork, entity);
        }

        public void Update(IUnitOfWork unitOfWork, FilmBO entity)
        {
            _repository.Update(unitOfWork, entity);
        }

        public void Delete(IUnitOfWork unitOfWork, int pk)
        {
            _repository.Delete(unitOfWork, pk);
        }

        public FilmBO Single(IUnitOfWork unitOfWork, int pk)
        {
            var result = _repository.Single(unitOfWork, pk);
            var formats = _formatManager.FindAll(unitOfWork);
            var genres = _genreManager.FindAll(unitOfWork);

            result.FormatName = formats.First(f => f.Id == result.FormatId).Name;
            result.QualityName = formats.First(f => f.Id == result.FormatId).Quality;
            result.GenreName01 = genres.First(g => g.Id == result.GenreId01).Name;
            result.GenreName02 = genres.First(g => g.Id == result.GenreId02).Name;

            return result;
        }

        public List<FilmBO> FindAll(IUnitOfWork unitOfWork)
        {
            var result = _repository.FindAll(unitOfWork);
            var formats = _formatManager.FindAll(unitOfWork);
            var genres = _genreManager.FindAll(unitOfWork);

            foreach (var r in result)
            {
                r.FormatName = formats.First(f => f.Id == r.FormatId).Name;
                r.QualityName = formats.First(f => f.Id == r.FormatId).Quality;
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