using Peplex_PFC.UI.UIO;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class ConfigUserAddWindow
    {
        public UserUIO NewUser { get; private set; }
        private WaitCursor _wc;

        public ConfigUserAddWindow()
        {
            InitializeComponent();
        }

        private void BntOkClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TxtNickName.Text) && !String.IsNullOrWhiteSpace(TxtEmail.Text) && !String.IsNullOrWhiteSpace(TxtEmail.Text))
            {
                var photo = PeplexUtils.ConvertBitmapImageToByteArray(new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "..\\..", "Resources\\DefaultProfile.png"))));

                NewUser = new UserUIO
                {
                    NickName = TxtNickName.Text,
                    Email = TxtEmail.Text,
                    Name = TxtName.Text,
                    Password = PeplexUtils.GeneratePassword(),
                    Photo =  photo,
                    Permissions = 0
                };

                SendEmail(NewUser);
            }
        }

        private void SendEmail(UserUIO user)
        {
            _wc = new WaitCursor();
            GMain.IsEnabled = false;

            var bw = new BackgroundWorker();
            bw.DoWork += SendEmailOnDoWork;
            bw.RunWorkerCompleted += SendEmailRunWorkerCompleted;
            bw.RunWorkerAsync(new
            {
                User = user
            });
        }

        private void SendEmailOnDoWork(object sender, DoWorkEventArgs e)
        {
            var o = e.Argument as dynamic;

            var body = "<h2>Bienvenido a PePlex</h2><br>Apodo: " + o.User.NickName + "<br>Password: " + o.User.Password;
            var subject = "Peplex, alta de nuevo usuario";

            MailMessage correos = new MailMessage();
            SmtpClient envios = new SmtpClient();

            try
            {
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = body;
                correos.Subject = subject;
                correos.IsBodyHtml = true;
                correos.To.Add(o.User.Email);

                /*if (rutaEjecutable.Equals("") == false)
                {
                    System.Net.Mail.Attachment archivo = new System.Net.Mail.Attachment(rutaEjecutable);
                    correos.Attachments.Add(archivo);
                }*/

                correos.From = new MailAddress(PeplexConfig.Instance.AdminEmail);
                envios.Credentials = new NetworkCredential(PeplexConfig.Instance.AdminEmail, PeplexConfig.Instance.AdminEmailPass);

                //Datos importantes no modificables para tener acceso a las cuentas

                envios.Host = "smtp.live.com";
                envios.Port = 587;
                envios.EnableSsl = true;

                envios.Send(correos);

                e.Result = true;
            }
            catch (Exception)
            {
                e.Result = false;
            }
        }

        private void SendEmailRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ GMain.IsEnabled = true; }), DispatcherPriority.ApplicationIdle);

            var result = (bool)e.Result;

            if (result)
            {
                CompositionRoot.Instance.Resolve<IUserServiceProxy>().Insert(new ProxyContext(), NewUser);
                MessageBoxWindow.Show(this, "INFO", DialogIcon.Info, new[] { DialogButton.Accept }, "Alta de nuevo usuario realizada con éxito.");
                DialogResult = true;
            }
            else
            {
                MessageBoxWindow.Show(this, "ERROR", DialogIcon.CommError, new[] { DialogButton.Accept }, "Error al dar de alta al nuevo usuario o no se puedo enviar el correo.");
                DialogResult = false;
            }
        }

        private void ConfigUserAddWindowPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }
    }
}
