using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;

namespace Peplex_PFC.UI.Config
{
    [Serializable]
    public sealed class PeplexConfig
    {
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

        private string _adminEmail;
        public string AdminEmail
        {
            get { return _adminEmail;}
            set { _adminEmail = value; }
        }

        private string _adminEmailPass;
        public string AdminEmailPass
        {
            get { return _adminEmailPass; }
            set { _adminEmailPass = value; }
        }

        public UserUIO CurrentUser { get; set; }
        public List<FilmUIO> Films { get; set; }
        public List<SerieUIO> Series { get; set; }

        #region Singelton construction, Load and Save methods
        private static readonly object InstanceAccess = new Object();

        private static volatile PeplexConfig _instance;
        public static PeplexConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceAccess)
                    {
                        if (_instance == null)
                        {
                            _instance = new PeplexConfig();
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
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Avelon RMS Cloud", "Headoffice.UI.Config");
            return @"..\..\PeplexUIConfig.config";
        }

        public void Save()
        {
            // Ensure the directory exists...
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath()));

            // Write the file with $$$ extension...
            var sid = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null);
            var allUsers = sid.Translate(typeof(System.Security.Principal.NTAccount)).Value;
            var fs = new System.Security.AccessControl.FileSecurity();
            fs.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(allUsers, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));

            var xmlSettings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

            using (var stream = new FileStream(FilePath() + "$$$", FileMode.OpenOrCreate, System.Security.AccessControl.FileSystemRights.FullControl, FileShare.ReadWrite, 8, FileOptions.None, fs))
            using (var writer = XmlWriter.Create(stream, xmlSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("configuration");

                writer.WriteElementString("rootMainLocal", _rootMainLocal);
                writer.WriteElementString("rootVideosLocal", _rootVideosLocal);
                writer.WriteElementString("rootImageLocal", _rootImageLocal);
                writer.WriteElementString("rootMusicLocal", _rootMusicLocal);
                writer.WriteElementString("rootFilmsLocal", _rootFilmsLocal);
                writer.WriteElementString("rootSeriesLocal", _rootSeriesLocal);

                writer.WriteElementString("adminEmail", _adminEmail);
                writer.WriteElementString("adminEmailPass", _adminEmailPass);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // Delete the previous file...
            File.Delete(FilePath());

            // Rename the $$$ to the correct file...
            File.Move(FilePath() + "$$$", FilePath());

            CompositionRoot.Instance.Resolve<IUserServiceProxy>().Update(new ProxyContext(), CurrentUser);
        }

        private void Load()
        {
            try
            {
                using (var fs = new FileStream(FilePath(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var document = new XmlDocument();
                    document.Load(fs);

                    var node = document.SelectSingleNode("/configuration/rootMainLocal");
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

                    node = document.SelectSingleNode("/configuration/adminEmail");
                    if (node != null)
                        _adminEmail = node.InnerText;

                    node = document.SelectSingleNode("/configuration/adminEmailPass");
                    if (node != null)
                        _adminEmailPass = node.InnerText;
                }
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
                    Save();

                throw;
            }
        }
    }
}
