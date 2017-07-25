using System.Collections.Generic;
using System.Linq;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;

namespace Peplex_PFC.BLL.Manager
{
    public class ExternalPlatformManager : IExternalPlatformManager
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ISerieRepository _serieRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IUserRepository _userRepository;

        public ExternalPlatformManager(IFilmRepository filmRepository, ISerieRepository serieRepository, IEpisodeRepository episodeRepository, IUserRepository userRepository)
        {
            _filmRepository = filmRepository;
            _serieRepository = serieRepository;
            _episodeRepository = episodeRepository;
            _userRepository = userRepository;
        }

        public List<ExternalPlatformFilm> GetFilms(IUnitOfWork unitOfWork)
        {
            var result = new List<ExternalPlatformFilm>();
            var bos = _filmRepository.FindAll(unitOfWork);
            return result;
        }

        public List<ExternalPlatformSerie> GetSeries(IUnitOfWork unitOfWork)
        {
            var result = new List<ExternalPlatformSerie>();
            var bos = _serieRepository.FindAll(unitOfWork);
            return result;
        }

        public List<ExternalPlatformEpisode> GetEpisodes(IUnitOfWork unitOfWork, int serieId)
        {
            var result = new List<ExternalPlatformEpisode>();
            var bos = _episodeRepository.FindAll(unitOfWork).Where(e => e.SerieId == serieId);
            return result;
        }

        public List<ExternalPlatformUser> GetUsers(IUnitOfWork unitOfWork)
        {
            var result = new List<ExternalPlatformUser>();
            var bos = _userRepository.FindAll(unitOfWork);
            return result;
        }

    }
}
