using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Peplex_PFC.BLL.Composition;
using Peplex_PFC.BLL.Composition.Config;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;
using Utils;
using System.Data.OleDb;

namespace Peplex_PFC.SL
{
    public static class ServiceLayer
    {
        public static void SetupRegularEndpoints()
        {
            var services = new List<ServiceEndpointDescriptor>
            {
                new ServiceEndpointDescriptor {ServiceType = typeof (GenreService), InterfaceType = typeof (IGenreService), ServiceName = "Peplex_PFCGenreService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (UserService), InterfaceType = typeof (IUserService), ServiceName = "Peplex_PFCUserService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (FormatService), InterfaceType = typeof (IFormatService), ServiceName = "Peplex_PFCFormatService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (FilmService), InterfaceType = typeof (IFilmService), ServiceName = "Peplex_PFCFilmService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (SerieService), InterfaceType = typeof (ISerieService), ServiceName = "Peplex_PFCSerieService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (EpisodeService), InterfaceType = typeof (IEpisodeService), ServiceName = "Peplex_PFCEpisodeService"},
                new ServiceEndpointDescriptor {ServiceType = typeof (PrerequisitesCheckService), InterfaceType = typeof (IPrerequisitesCheckService), ServiceName = "AvelonRMSPrerequisitesCheckService"},
            };

            BuildEndpoints(services.ToArray(), ServiceConfig.Instance.ServiceLayerPort, "");
        }

        public static void BuildEndpoints(ServiceEndpointDescriptor[] endpointDescriptors, int servicePort, string certificateCommonName)
        {
            var baseBinding = new NetTcpBinding(SecurityMode.None) { MaxReceivedMessageSize = 2147483647 };
            var bindingElements = baseBinding.CreateBindingElements();

            var binding = new CustomBinding(bindingElements) { Name = "GZipAndEncryptionBinding", Namespace = "http://tempuri.org/bindings", };
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.SendTimeout = new TimeSpan(0, 10, 0);

            foreach (var service in endpointDescriptors)
            {
                var contractDesc = ContractDescription.GetContract(service.InterfaceType);
                var serviceHost = new ServiceHost(service.ServiceType);

                // Add tcp endpoint
                //var tcpEndpoint = new ServiceEndpoint(contractDesc, binding, new EndpointAddress(new Uri(String.Format("net.tcp://localhost:{0}/{1}", servicePort, service.ServiceName))));
                var tcpEndpoint = new ServiceEndpoint(contractDesc, binding, new EndpointAddress(new Uri(String.Format("net.tcp://{2}:{0}/{1}", servicePort, service.ServiceName, ServiceConfig.Instance.ServiceAddress))));

                serviceHost.AddServiceEndpoint(tcpEndpoint);

                serviceHost.Open();
            }
        }

        public static void SetupRestEndpoints()
        {
            var services = new[]
            {
                new ServiceEndpointDescriptor {ServiceType = typeof (ExternalPlatformRestService), InterfaceType = typeof (IExternalPlatformRestService), ServiceName = "PeplexExternalPlatformRestService"},
            };

            BuildEndpointsRest(services, ServiceConfig.Instance.ExternalPlatformRestApiPort, "");
        }

        public static void BuildEndpointsRest(ServiceEndpointDescriptor[] endpointDescriptors, int servicePort, string sslCertificateCommonName)
        {
            var custom = new CustomBinding(new WebHttpBinding(WebHttpSecurityMode.Transport));
            (custom.Elements[1] as TransportBindingElement).MaxReceivedMessageSize = int.MaxValue;

            foreach (var service in endpointDescriptors)
            {
                //var baseAddress = String.Format("https://localhost:{0}/{1}", servicePort, service.ServiceName);
                var baseAddress = String.Format("https://{2}:{0}/{1}", servicePort, service.ServiceName, ServiceConfig.Instance.ServiceAddress);
                var host = new ServiceHost(service.ServiceType, new Uri(baseAddress));
                host.AddServiceEndpoint(service.InterfaceType, custom, "").Behaviors.Add(new WebHttpBehavior());
                host.Open();

                Cert(servicePort, sslCertificateCommonName);
            }
        }

        private static void Cert(int servicePort, string sslCertificateCommonName)
        {
            var storex = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                storex.Open(OpenFlags.ReadOnly);

                var certificatesx = storex.Certificates.Find(X509FindType.FindBySubjectName, sslCertificateCommonName, false);

                if (certificatesx.Count == 0)
                    throw new Exception(String.Format("Certificate {0} not found", sslCertificateCommonName));

                // Netsh http delete sslcert ipport=0.0.0.0:<port>
                var unbindPortToCertificate = new Process
                {
                    StartInfo =
                        {
                            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "netsh.exe"),
                            Arguments = string.Format("Netsh http delete sslcert ipport=0.0.0.0:{0}", servicePort),
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                };

                unbindPortToCertificate.Start();
                unbindPortToCertificate.WaitForExit();

                // netsh http add sslcert ipport=0.0.0.0:<port> certhash={<thumbprint>} appid={<some GUID>} clientcertnegotiation=enable
                var bindPortToCertificate = new Process
                {
                    StartInfo =
                        {
                            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "netsh.exe"),
                            Arguments = string.Format("http add sslcert ipport=0.0.0.0:{0} certhash={1} appid={{{2}}} clientcertnegotiation=enable", servicePort, certificatesx[0].Thumbprint, Assembly.GetExecutingAssembly().GetType().GUID),
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                };

                bindPortToCertificate.Start();
                bindPortToCertificate.WaitForExit();
            }
            finally
            {
                storex.Close();
            }
        }

        #region UPDATE DATABASE
        /// <summary>Recorre los directorios de películas y series y actualiza la base de datos insertando o borrando nuevos datos</summary>
        /// <param name="opc">Entero que indica si se quiere realizar la carga para actualizar la BD (1->SI, 0->NO)</param> 
        public static bool InitialLoad(int opc)
        {
            try
            {
                if (opc == 1) //Se recorre la BD para comprobar si existen nuevas películas o series
                {
                    //if (DataBaseCreated())
                    //    LblMsg.Content = "LA BD ESTÁ CREADA";

                    Console.WriteLine("\nESTA ACTIVADO EL MODO ACTUALIZAR CATÁLGO.");
                    Console.WriteLine("BORRANDO PELÍCULAS NO ENCONTRADAS EN LA BIBLIOTECA...");
                    //Borramos series y películas de la BD que no estén en los directorios correspondientes
                    Console.WriteLine("BORRANDO PELÍCULAS NO ENCONTRADAS EN LA BIBLIOTECA...");
                    DeleteFilms();
                    Console.WriteLine("BORRANDO SERIES NO ENCONTRADAS EN LA BIBLIOTECA...");
                    DeleteSeries();
                    Console.WriteLine("BORRANDO CAPÍTULOS NO ENCONTRADAS EN LA BIBLIOTECA...");
                    DeleteEpisodes();
                    //Insertamos series y películas a la BD que sean nuevas en los direcotorios correspondientes
                    Console.WriteLine("INSERTANDO PELÍCULAS NUEVAS EN LA BIBLIOTECA...");
                    InsertFilms();
                    Console.WriteLine("INSERTANDO SERIES Y CAPÍTULOS NUEVOS EN LA BIBLIOTECA...");
                    InsertSeries();
                    Console.WriteLine("ACTUALIZANDO DATOS CONTENIDO MULTIMEDIA...");
                    ImportData();
                    Console.WriteLine("\nHA FINALIZADO LA ACTUALIZACIÓN DEL CONTENIDO MULTIMEDIA.");
                    Console.WriteLine("PULSE CUALQUIER TECLA PARA ARRANCAR LOS SERVICIOS");
                    Console.ReadKey();
                    Console.Clear();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                Console.WriteLine("\nERROR AL ACTUALIZAR EL CONTENIDO MULTIMEDIA.\nPULSE CUALQUIER TECLA PARA ARRANCAR LOS SERVICIOS");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
        }

        /// <summary>Funcion que comprueba si la Base de datos peplexBD se encuentra ya creada</summary>
        /// <returns>True si está creada y False en caso contrario</returns>
        public static bool DataBaseCreated()
        {
            return true;
        }

        #region Métodos Películas
        /// <summary>
        /// Recorre el directorio de las películas para ver que películas se han borrado y actualizar la BD
        /// </summary>
        public static void DeleteFilms()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var films = CompositionRoot.Instance.Resolve<IFilmManager>().FindAll(uow);
                var formats = CompositionRoot.Instance.Resolve<IFormatManager>().FindAll(uow);
                DirectoryInfo dirInfo = new DirectoryInfo(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootVideosLocal + ServiceConfig.Instance.RootFilmsLocal);//Ruta de las películas

                foreach (var f in films)
                {
                    var formatName = formats.FirstOrDefault(format => format.Id == f.FormatId).Name;
                    var regExpr = String.Format("{0} -*.{1}", f.Title, formatName);

                    var files = dirInfo.GetFiles(regExpr); //Comprobamos si existe

                    if (!files.Any())//Si no existe la película se borrar de la BD
                        CompositionRoot.Instance.Resolve<IFilmManager>().Delete(uow, f.Id);
                }

                uow.Commit();
            }
        }

        /// <summary>Recorre el directorio de películas en busca de películas nuevas</summary>
        public static void InsertFilms()
        {
            string[] files = Directory.GetFiles(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootVideosLocal + ServiceConfig.Instance.RootFilmsLocal, "*.*");

            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var films = CompositionRoot.Instance.Resolve<IFilmManager>().FindAll(uow);
                var formats = CompositionRoot.Instance.Resolve<IFormatManager>().FindAll(uow);

                foreach (string s in files)
                {
                    FileInfo fi = null;
                    try
                    {
                        fi = new FileInfo(s);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }

                    string[] peliculaTituloFormato = new string[2];
                    peliculaTituloFormato = fi.Name.Split('.');
                    string titulo = peliculaTituloFormato[0];
                    string formato = peliculaTituloFormato[1];
                    string[] peliculaTituloSubtitulo = new string[2];
                    peliculaTituloSubtitulo = titulo.Split('-');
                    titulo = peliculaTituloSubtitulo[0].Trim();
                    string subtitulo = peliculaTituloSubtitulo[1].Trim();

                    var filmsWithSameTitle = films.Where(f => f.Title.Equals(titulo, StringComparison.CurrentCultureIgnoreCase));

                    if (filmsWithSameTitle.Any()) // Si existe alguna película con el mismo título
                    {
                        var filmsWithSameFormat =
                            filmsWithSameTitle.Where(
                                f => f.FormatName.Equals(formato, StringComparison.CurrentCultureIgnoreCase));

                        if (!filmsWithSameFormat.Any()) // Comprobamos que no tenga el mismo formato antes de insertarla
                        {
                            var newFormat = formats.Where(f => f.Name.Equals(formato, StringComparison.CurrentCultureIgnoreCase));
                            var cover = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + titulo + " - Cover.jpg");
                            var background = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + titulo + " - Background.jpg");
                            var coverOpt = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + titulo + " - Cover - Opt.jpg");
                            var backgroundOpt = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + titulo + " - Background - Opt.jpg");

                            var newFilm = new FilmBO
                            {
                                Title = titulo,
                                Subtitle = subtitulo,
                                FormatId = newFormat.ToList()[0].Id,
                                GenreId01 = 1,
                                GenreId02 = 1,
                                PremiereDate = DateTime.Now,
                                DownloadDate = DateTime.Now,
                                Cover = cover,
                                Background = background,
                                CoverOpt = coverOpt,
                                BackgroundOpt = backgroundOpt
                            };

                            CompositionRoot.Instance.Resolve<IFilmManager>().Insert(uow, newFilm);
                        }
                    }
                    else // Si no existe la pélicula la insertamos
                    {
                        var newFormat = formats.Where(f => f.Name.Equals(formato, StringComparison.CurrentCultureIgnoreCase));
                        var cover = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + titulo + " - Cover.jpg");
                        var background = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + titulo + " - Background.jpg");
                        var coverOpt = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + titulo + " - Cover - Opt.jpg");
                        var backgroundOpt = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + titulo + " - Background - Opt.jpg");

                        var newFilm = new FilmBO
                        {
                            Title = titulo,
                            Subtitle = subtitulo,
                            FormatId = newFormat.ToList()[0].Id,
                            GenreId01 = 1,
                            GenreId02 = 1,
                            PremiereDate = DateTime.Now,
                            DownloadDate = DateTime.Now,
                            Cover = cover,
                            Background = background,
                            CoverOpt = coverOpt,
                            BackgroundOpt = backgroundOpt
                        };

                        CompositionRoot.Instance.Resolve<IFilmManager>().Insert(uow, newFilm);
                    }
                }

                uow.Commit();
            }
        }
        #endregion

        #region Métodos Series
        /// <summary>
        /// Recorre el directorio de las series para ver que series se han borrado y actualizar la BD
        /// </summary>
        public static void DeleteSeries()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var series = CompositionRoot.Instance.Resolve<ISerieManager>().FindAll(uow);
                DirectoryInfo dirInfo = new DirectoryInfo(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootVideosLocal + ServiceConfig.Instance.RootSeriesLocal); // Ruta de las series

                foreach (var s in series)
                {
                    var directories = dirInfo.GetDirectories(s.Title); // Comprobamos si existe

                    if (!directories.Any()) // Si no existe la serie se borra de la BD
                        CompositionRoot.Instance.Resolve<ISerieManager>().Delete(uow, s.Id);
                }

                uow.Commit();
            }
        }

        /// <summary>Recorre el directorio de series en busca de series nuevas y sus capítulos</summary>
        public static void InsertSeries()
        {
            int serieId;

            DirectoryInfo dirInfo = new DirectoryInfo(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootVideosLocal + ServiceConfig.Instance.RootSeriesLocal); // Ruta de las series
            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();

            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var series = CompositionRoot.Instance.Resolve<ISerieManager>().FindAll(uow);
                var formats = CompositionRoot.Instance.Resolve<IFormatManager>().FindAll(uow);
                var episodes = CompositionRoot.Instance.Resolve<IEpisodeManager>().FindAll(uow);

                foreach (DirectoryInfo d in dirInfos)
                {
                    var seriesWithSameTitle = series.Where(s => s.Title.Equals(d.Name, StringComparison.CurrentCultureIgnoreCase));

                    if (!seriesWithSameTitle.Any()) // Si no existe la serie la insertamos
                    {
                        var cover = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + d.Name + " - Cover.jpg");
                        var background = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + d.Name + " - Background.jpg");
                        var coverOpt = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + d.Name + " - Cover - Opt.jpg");
                        var backgroundOpt = File.ReadAllBytes(ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootImageLocal + d.Name + " - Background - Opt.jpg");

                        var newSerie = new SerieBO
                        {
                            Title = d.Name,
                            GenreId01 = 1,
                            GenreId02 = 1,
                            PremiereDate = DateTime.Now,
                            DownloadDate = DateTime.Now,
                            Cover = cover,
                            Background = background,
                            CoverOpt = coverOpt,
                            BackgroundOpt= backgroundOpt
                        };

                        serieId = CompositionRoot.Instance.Resolve<ISerieManager>().Insert(uow, newSerie);
                    }
                    else
                        serieId = seriesWithSameTitle.ToList()[0].Id;

                    //Insertamos los capítulos
                    InsertEpisodes(uow, serieId, d, formats, episodes.Where(e => e.SerieId == serieId).ToList());
                }

                uow.Commit();
            }
        }
        #endregion

        #region Métodos Capítulos
        /// <summary>
        /// Recorre el directorio de las capítulos de una seriepara a ver que capítulos se han borrado y actualizar la BD
        /// </summary>
        public static void DeleteEpisodes()
        {
            var rutaSeries = ServiceConfig.Instance.RootMainLocal + ServiceConfig.Instance.RootVideosLocal + ServiceConfig.Instance.RootSeriesLocal;
            var fmt = "00";

            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var series = CompositionRoot.Instance.Resolve<ISerieManager>().FindAll(uow);
                var formats = CompositionRoot.Instance.Resolve<IFormatManager>().FindAll(uow);
                var episodes = CompositionRoot.Instance.Resolve<IEpisodeManager>().FindAll(uow);

                foreach (var episode in episodes.OrderBy(e => e.SerieId).ThenBy(e => e.Season).ThenBy(e => e.Number)) // Recorremos los capítulos
                {
                    var serie = series.FirstOrDefault(s => s.Id == episode.SerieId);
                    var rootEpisode = Path.Combine(rutaSeries, serie.Title, String.Format("T{0}", episode.Season));
                    var formatName = formats.FirstOrDefault(format => format.Id == episode.FormatId).Name;
                    var fileNameCompleted = String.Format("{0}x{1} - {2}.{3}", episode.Season.ToString(fmt), episode.Number.ToString(fmt), episode.Name, formatName);

                    DirectoryInfo dirInfo = new DirectoryInfo(rootEpisode);

                    //Ruta de los capítulos de las serie y la temporada correspondiente
                    var files = dirInfo.GetFiles(fileNameCompleted); // Comprobamos si existe

                    if (!files.Any())//Si no existe el capítulo se borrar de la BD
                        CompositionRoot.Instance.Resolve<IEpisodeManager>().Delete(uow, episode.SerieId, episode.Season, episode.Number);
                }

                uow.Commit();
            }
        }

        ///<sumary>Comprueba si existe el capítulo en la BD</sumary>
        /// <param name="codSerie">Entero, Id de la serie</param>
        public static void InsertEpisodes(IUnitOfWork uow, int serieId, DirectoryInfo d, List<FormatBO> formats, List<EpisodeBO> episodes)
        {
            foreach (DirectoryInfo dTemporada in d.GetDirectories())
            {
                foreach (FileSystemInfo f in dTemporada.GetFiles())
                {
                    string[] nombreFormatoCapitulo = new string[2];
                    nombreFormatoCapitulo = f.Name.Split('.');
                    string nombre = nombreFormatoCapitulo[0];
                    string formato = nombreFormatoCapitulo[1].Trim();//FORMATO
                    string[] temporadaNumeroNombreCapitulo = new string[2];
                    temporadaNumeroNombreCapitulo = nombre.Split('-');
                    nombre = temporadaNumeroNombreCapitulo[1].Trim();//NOMBRE DEL CAPÍTULO
                    string temporadaNumeroCapitulo = temporadaNumeroNombreCapitulo[0];
                    int codTemporada = Convert.ToInt32(temporadaNumeroCapitulo.Split('x')[0].Trim());//TEMPORADA
                    int codNumero = Convert.ToInt32(temporadaNumeroCapitulo.Split('x')[1].Trim());//NÚMERO DEL CAPÍTULO

                    var episodeWithSameSeassonAndNumber = episodes.FirstOrDefault(e => e.Season == codTemporada && e.Number == codNumero);

                    if (episodeWithSameSeassonAndNumber == null) // Si no existe el capítulo para la serie actual
                    {
                        var newFormat = formats.Where(format => format.Name.Equals(formato, StringComparison.CurrentCultureIgnoreCase));

                        var newEpisode = new EpisodeBO
                        {
                            SerieId = serieId,
                            Season = codTemporada,
                            Number = codNumero,
                            Name = nombre,
                            FormatId = newFormat.ToList()[0].Id
                        };

                        CompositionRoot.Instance.Resolve<IEpisodeManager>().Insert(uow, newEpisode);
                    }

                }
            }
        }

        #endregion

        #region Importar datos XML
        public static void ImportData()
        {
            string PathFile = @"..\..\PeplexData.xlsx";

            #region Obtain Data Excel
            List<string[]> lstValues = new List<string[]>();

            const string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR={1};IMEX=1'";

            using (var conn = new OleDbConnection(String.Format(connectionString, PathFile, "YES")))
            {
                conn.Open();

                var dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                var row = dt.Rows[0];
                var sheetName = Convert.ToString(row["TABLE_NAME"]);

                using (var cmd = new OleDbCommand("SELECT * FROM [" + sheetName + "]", conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var values = new string[rdr.FieldCount];
                        for (var i = 0; i < rdr.FieldCount; i++)
                            values[i] = Convert.ToString(rdr[i]);

                        lstValues.Add(values);
                    }
                }
            }
            #endregion

            #region Assign DataExcel
            try
            {
                // FindAll Serie & Film
                using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                {
                    var films = CompositionRoot.Instance.Resolve<IFilmManager>().FindAll(uow);
                    var series = CompositionRoot.Instance.Resolve<ISerieManager>().FindAll(uow);

                    foreach (var v in lstValues)
                    {
                        if (v[0] == "0")
                        {
                            var strType = v[2];
                            var title = v[1];
                            var genre01 = Convert.ToInt32(v[3]);
                            var genre02 = Convert.ToInt32(v[4]);

                            switch (strType)
                            {
                                case "F":
                                    var film = films.SingleOrDefault(f => f.Title.Equals(title));
                                    if (film != null)
                                    {
                                        film.GenreId01 = genre01;
                                        film.GenreId02 = genre02;
                                        film.Note = Convert.ToDecimal(v[6]);
                                        film.Synopsis = v[7];
                                        film.PremiereDate = Convert.ToDateTime(v[8]);
                                        film.DurationMin = Convert.ToInt32(v[9]);
                                        film.TrailerURL = v[10];

                                        CompositionRoot.Instance.Resolve<IFilmManager>().Update(uow, film);
                                    }

                                    break;
                                case "S":
                                    var serie = series.SingleOrDefault(s => s.Title.Equals(title));
                                     if(serie != null)
                                     {
                                         serie.GenreId01 = genre01;
                                         serie.GenreId02 = genre02;
                                         serie.Note = Convert.ToDecimal(v[6]);
                                         serie.Synopsis = v[7];
                                         serie.PremiereDate = Convert.ToDateTime(v[8]);
                                         serie.DurationMin = Convert.ToInt32(v[9]);

                                        CompositionRoot.Instance.Resolve<ISerieManager>().Update(uow, serie);
                                    }
                                    
                                    break;
                            }
                        }
                    }

                    uow.Commit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion
        }
        #endregion
        #endregion
    }
}
