using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace Peplex_PFC.BLL.Composition.Config
{
    public class ServiceConfig
    {
        public static string ConfigFileName { get; set; }

        private string _culture = "es-ES";
        public string Culture
        {
            get { return _culture; }
            set { _culture = value; }
        }

        private string _dataSource = @"PEPE-PC\SQL2016";
        public string DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        private string _catalog = "CENTRAL_PEPLEX_DB";
        public string Catalog
        {
            get { return _catalog; }
            set { _catalog = value; }
        }

        private string _user = "sa";
        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _password = "pepe1989";

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private int _serviceLayerPort = 9090;
        public int ServiceLayerPort
        {
            get { return _serviceLayerPort; }
            set { _serviceLayerPort = value; }
        }

        private string _serviceAddress = "peplexpfc.ddns.net";
        public string ServiceAddress
        {
            get { return _serviceAddress; }
            set { _serviceAddress = value; }
        }

        private int _externalPlatformRestApiPort = 9095;
        public int ExternalPlatformRestApiPort
        {
            get { return _externalPlatformRestApiPort; }
            set { _externalPlatformRestApiPort = value; }
        }

        private string _updatesBasePath = "";
        public string UpdatesBasePath
        {
            get { return _updatesBasePath; }
            set { _updatesBasePath = value; }
        }

        public String ConnectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = _dataSource,
                    InitialCatalog = _catalog,
                    UserID = _user,
                    Password = _password,
                    IntegratedSecurity = false,
                    MultipleActiveResultSets = true
                };

                return builder.ToString();
            }
        }

        #region Directory Multimedia
        private int _updateDB;
        public int UpdateDB
        {
            get { return _updateDB; }
            set { _updateDB = value; }
        }

        private string _rootMainLocal;
        public string RootMainLocal
        {
            get { return _rootMainLocal; }
            set { _rootMainLocal = value; }
        }

        private string _rootVideosLocal;
        public string RootVideosLocal
        {
            get { return _rootVideosLocal; }
            set { _rootVideosLocal = value; }
        }

        private string _rootImageLocal;
        public string RootImageLocal
        {
            get { return _rootImageLocal; }
            set { _rootImageLocal = value; }
        }

        private string _rootMusicLocal;
        public string RootMusicLocal
        {
            get { return _rootMusicLocal; }
            set { _rootMusicLocal = value; }
        }

        private string _rootFilmsLocal;
        public string RootFilmsLocal
        {
            get { return _rootFilmsLocal; }
            set { _rootFilmsLocal = value; }
        }

        private string _rootSeriesLocal;
        public string RootSeriesLocal
        {
            get { return _rootSeriesLocal; }
            set { _rootSeriesLocal = value; }
        }
        #endregion

        #region Singelton construction, Load and Save methods
        private static readonly object InstanceAccess = new Object();

        private static volatile ServiceConfig _instance;

        static ServiceConfig()
        {
            ConfigFileName = "";
        }

        public static ServiceConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceAccess)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServiceConfig();
                            _instance.Load();
                        }
                    }
                }

                return _instance;
            }
        }
        #endregion

        private String FilePath()
        {
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Peplex", String.IsNullOrWhiteSpace(ConfigFileName) ? "Peplex.SL.Config" : ConfigFileName);
            return @"..\..\PeplexSLConfig.config";
        }

        public void Save()
        {
            // Ensure the directory exists...
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath()));

            // Write the file with $$$ extension...
            var sid = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null);
            String allUsers = sid.Translate(typeof(System.Security.Principal.NTAccount)).Value;
            var fs = new System.Security.AccessControl.FileSecurity();
            fs.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(allUsers, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));

            var xmlSettings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

            using (var stream = new FileStream(FilePath() + "$$$", FileMode.OpenOrCreate, System.Security.AccessControl.FileSystemRights.FullControl, FileShare.ReadWrite, 8, FileOptions.None, fs))
            using (var writer = XmlWriter.Create(stream, xmlSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("configuration");

                //writer.WriteElementString("Culture", _culture);
                writer.WriteElementString("dataSource", _dataSource);
                writer.WriteElementString("catalog", _catalog);
                writer.WriteElementString("user", _user);
                writer.WriteElementString("password", _password);
                writer.WriteElementString("serviceAddress", _serviceAddress);
                writer.WriteElementString("serviceLayerPort", _serviceLayerPort.ToString());
                writer.WriteElementString("externalPlatformRestApiPort", _externalPlatformRestApiPort.ToString());
                //writer.WriteElementString("UpdatesBasePath", _updatesBasePath);

                writer.WriteElementString("updateDB", UpdateDB.ToString());
                writer.WriteElementString("rootMainLocal", _rootMainLocal);
                writer.WriteElementString("rootVideosLocal", _rootVideosLocal);
                writer.WriteElementString("rootImageLocal", _rootImageLocal);
                writer.WriteElementString("rootMusicLocal", _rootMusicLocal);
                writer.WriteElementString("rootFilmsLocal", _rootFilmsLocal);
                writer.WriteElementString("rootSeriesLocal", _rootSeriesLocal);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // Delete the previous file...
            File.Delete(FilePath());

            // Rename the $$$ to the correct file...
            System.Threading.Thread.Sleep(500);
            File.Move(FilePath() + "$$$", FilePath());
        }

        private void Load()
        {
            try
            {
                using (var fs = new FileStream(FilePath(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var document = new XmlDocument();
                    document.Load(fs);

                    //var node = document.SelectSingleNode("/Config/Culture");
                    //if (node != null)
                    //    _culture = node.InnerText;

                    var node = document.SelectSingleNode("/configuration/dataSource");
                    if (node != null)
                        _dataSource = node.InnerText;

                    node = document.SelectSingleNode("/configuration/catalog");
                    if (node != null)
                        _catalog = node.InnerText;

                    node = document.SelectSingleNode("/configuration/user");
                    if (node != null)
                        _user = node.InnerText;

                    node = document.SelectSingleNode("/configuration/password");
                    if (node != null)
                        _password = node.InnerText;

                    node = document.SelectSingleNode("/configuration/serviceAddress");
                    if (node != null && !String.IsNullOrEmpty(node.InnerText))
                        _serviceAddress = node.InnerText;

                    node = document.SelectSingleNode("/configuration/serviceLayerPort");
                    if (node != null && !String.IsNullOrEmpty(node.InnerText))
                        _serviceLayerPort = Convert.ToInt32(node.InnerText);

                    node = document.SelectSingleNode("/configuration/externalPlatformRestApiPort");
                    if (node != null && !String.IsNullOrEmpty(node.InnerText))
                        _externalPlatformRestApiPort = Convert.ToInt32(node.InnerText);

                    //node = document.SelectSingleNode("/Config/UpdatesBasePath");
                    //if (node != null && !String.IsNullOrEmpty(node.InnerText))
                    //    _updatesBasePath = node.InnerText;

                    node = document.SelectSingleNode("/configuration/updateDB");
                    if (node != null && !String.IsNullOrEmpty(node.InnerText))
                        _updateDB = Convert.ToInt32(node.InnerText);


                    node = document.SelectSingleNode("/configuration/rootMainLocal");
                    if (node != null)
                        _rootMainLocal = node.InnerText;

                    node = document.SelectSingleNode("/configuration/rootVideosLocal");
                    if (node != null)
                        _rootVideosLocal = node.InnerText;

                    node = document.SelectSingleNode("/configuration/rootImageLocal");
                    if (node != null)
                        _rootImageLocal = node.InnerText;

                    node = document.SelectSingleNode("/configuration/rootMusicLocal");
                    if (node != null)
                        _rootMusicLocal = node.InnerText;

                    node = document.SelectSingleNode("/configuration/rootFilmsLocal");
                    if (node != null)
                        _rootFilmsLocal = node.InnerText;

                    node = document.SelectSingleNode("/configuration/rootSeriesLocal");
                    if (node != null)
                        _rootSeriesLocal = node.InnerText;
                }
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
                    Save();
            }
        }
    }
}
