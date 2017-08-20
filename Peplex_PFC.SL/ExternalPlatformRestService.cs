using System;
using System.ServiceModel;
using Peplex_PFC.BLL.Composition;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.SL
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ExternalPlatformRestService : IExternalPlatformRestService
    {
        private readonly IExternalPlatformManager _manager = CompositionRoot.Instance.Resolve<IExternalPlatformManager>();
        private const string DATE_FORMAT = "yyyy-MM-dd";

        public ExternalPlatformFilmCollection GetFilms()
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformFilmCollection();

                    foreach (var f in _manager.GetFilms(uow))
                    {
                        var film = new ExternalPlatformFilm
                        {
                            Id = f.Id,
                            Title = f.Title,
                            Synopsis = f.Synopsis,
                            Note = f.Note,
                            GenreName01 = f.GenreName01,
                            GenreName02 = f.GenreName02,
                            DownloadDate = f.DownloadDate.ToString(DATE_FORMAT),
                            Base64Cover = Convert.ToBase64String(f.Cover)
                        };

                        result.Films.Add(film);
                    }

                    result.Count = result.Films.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformFilmCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformFilmCollection GetFilm(string filmId)
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformFilmCollection();

                    var f = _manager.GetFilm(uow, Convert.ToInt32(filmId));

                    if (f == null)
                        throw new Exception(String.Format("No existe la película con Id = {0}", filmId));

                    var film = new ExternalPlatformFilm
                    {
                        Id = f.Id,
                        Title = f.Title,
                        Subtitle = f.Subtitle,
                        Synopsis = f.Synopsis,
                        Note = f.Note,
                        TrailerURL = f.TrailerURL,
                        FormatName = f.FormatName,
                        DownloadDate = f.DownloadDate.ToString(DATE_FORMAT),
                        PremiereDate = f.PremiereDate.ToString(DATE_FORMAT),
                        DurationMin = f.DurationMin,
                        GenreName01 = f.GenreName01,
                        GenreName02 = f.GenreName02,
                        Base64Cover = Convert.ToBase64String(f.Cover),
                        Base64Background = Convert.ToBase64String(f.Background)
                    };

                    result.Films.Add(film);

                    result.Count = result.Films.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformFilmCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformSerieCollection GetSeries()
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformSerieCollection();

                    foreach (var s in _manager.GetSeries(uow))
                    {
                        var serie = new ExternalPlatformSerie
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Synopsis = s.Synopsis,
                            Note = s.Note,
                            GenreName01 = s.GenreName01,
                            GenreName02 = s.GenreName02,
                            DownloadDate = s.DownloadDate.ToString(DATE_FORMAT),
                            Base64Cover = Convert.ToBase64String(s.Cover)
                        };

                        result.Series.Add(serie);
                    }

                    result.Count = result.Series.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformSerieCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformSerieCollection GetSerie(string serieId)
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformSerieCollection();

                    var s =_manager.GetSerie(uow, Convert.ToInt32(serieId));

                    if (s == null)
                        throw new Exception(String.Format("No existe la serie con Id = {0}", serieId));

                    var serie = new ExternalPlatformSerie
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Synopsis = s.Synopsis,
                        Note = s.Note,
                        DownloadDate = s.DownloadDate.ToString(DATE_FORMAT),
                        PremiereDate = s.PremiereDate.ToString(DATE_FORMAT),
                        DurationMin = s.DurationMin,
                        GenreName01 = s.GenreName01,
                        GenreName02 = s.GenreName02,
                        Base64Cover = Convert.ToBase64String(s.Cover),
                        Base64Background = Convert.ToBase64String(s.Background)
                    };

                    foreach (var ep in s.Episodes)
                    {
                        serie.Episodes.Add(new ExternalPlatformEpisode
                        {
                            SerieId = ep.SerieId,
                            Season = ep.Season,
                            Number = ep.Number,
                            Name = ep.Name,
                            FormatName = ep.FormatName
                        });
                    }

                    result.Series.Add(serie);

                    result.Count = result.Series.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformSerieCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformUserCollection GetUsers()
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformUserCollection();

                    foreach (var u in _manager.GetUsers(uow))
                    {
                        var user = new ExternalPlatformUser
                        {
                            Id = u.Id,
                            NickName = u.NickName,
                            Password = u.Password
                        };

                        result.Users.Add(user);
                    }

                    result.Count = result.Users.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformUserCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformUserCollection GetUser(string userId)
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformUserCollection();

                    var u = _manager.GetUser(uow, Convert.ToInt32(userId));

                    if (u == null)
                        throw new Exception(String.Format("No existe el usuario con Id = {0}", userId));

                    var user = new ExternalPlatformUser
                    {
                        Id = u.Id,
                        NickName = u.NickName,
                        Password = u.Password,
                        Name = u.Name,
                        Email = u.Email,
                        Base64Photo = Convert.ToBase64String(u.Photo)
                    };

                    user.FilmSeen.AddRange(u.FilmSeen);
                    user.SerieSeen.AddRange(u.SerieSeen);
                    user.EpisodeSeen.AddRange(u.EpisodeSeen);

                    result.Users.Add(user);

                    result.Count = result.Users.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformUserCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }

        public ExternalPlatformGenreCollection GetGenres()
        {
            try
            {
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var result = new ExternalPlatformGenreCollection();

                    foreach (var g in _manager.GetGenres(uow))
                    {
                        var genre = new ExternalPlatformGenre
                        {
                            Id = g.Id,
                            Name = g.Name
                        };

                        result.Genres.Add(genre);
                    }

                    result.Count = result.Genres.Count;
                    result.Result = 0;
                    result.ResultString = "Ok";

                    return result;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : "");

                return new ExternalPlatformGenreCollection
                {
                    Count = 0,
                    Result = -1,
                    ResultString = message
                };
            }
        }
    }
}
