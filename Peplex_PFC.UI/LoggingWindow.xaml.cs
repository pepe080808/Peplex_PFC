using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class LoggingWindow
    {
        const int MAX_OPPORTUNITIES = 3;
        private int __oportunidades;

        private List<UserUIO> _users { get; set; }

        //private WaitIndicatorOnThread _bussy;
        private WaitCursor _wc;

        public LoggingWindow(List<UserUIO> users)
        {
            _users = users;
            InitializeComponent();
        }

        #region Load user data
        private void LogginghWindowLoad(object sender, RoutedEventArgs e)
        {
            LoadDataUsers();
            TxtUser.Focus();
        }

        private void LoadDataUsers()
        {
            _wc = new WaitCursor();
            GInfo.IsEnabled = false;

            var bw = new BackgroundWorker();
            bw.DoWork += LoadDataUsersOnDoWork;
            bw.RunWorkerCompleted += LoadDataUsersRunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void LoadDataUsersOnDoWork(object sender, DoWorkEventArgs e)
        {
            var proxyContext = new ProxyContext();

            var users = CompositionRoot.Instance.Resolve<IUserServiceProxy>().FindAll(proxyContext).ToList();

            if (proxyContext.HasErrors)
                e.Result = proxyContext;
            else
            {
                e.Result = users;
            }
        }

        private void LoadDataUsersRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ GInfo.IsEnabled = true; TxtUser.Focus(); }), DispatcherPriority.ApplicationIdle);

            if (e.Result is ProxyContext)
            {
                (e.Result as ProxyContext).ShowErrors(Window.GetWindow(Parent));
                return;
            }

            var result = e.Result as List<UserUIO>;

            if (result == null)
                return;

            _users = result;
        }
        #endregion

        #region Enter application
        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TxtUser.Text))
                TxtUser.Text = "";

            if (String.IsNullOrEmpty(TxtPass.Password))
                TxtPass.Password = "";

            var currentUser = _users.FirstOrDefault(u => u.NickName.Equals(TxtUser.Text, StringComparison.CurrentCultureIgnoreCase) && u.Password.Equals(TxtPass.Password, StringComparison.CurrentCultureIgnoreCase));
            if (currentUser != null)
            {
                LoadData(currentUser);
            }
            else
            {
                __oportunidades++;
                MessageBox.Show("Usuario y Contraseña no validos.\nIntentos : " + __oportunidades + "/" + MAX_OPPORTUNITIES + ".", " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                if (__oportunidades == MAX_OPPORTUNITIES)
                    Application.Current.Shutdown();
            }
        }

        private void LoadData(UserUIO user)
        {
            _wc = new WaitCursor();

            var bw = new BackgroundWorker();
            bw.DoWork += LoadDataOnDoWork;
            bw.RunWorkerCompleted += LoadDataRunWorkerCompleted;
            bw.RunWorkerAsync(new
            {
                UserId = user.Id
            });
        }

        private void LoadDataOnDoWork(object sender, DoWorkEventArgs e)
        {
            var o = e.Argument as dynamic;
            var proxyContext = new ProxyContext();

            var user = CompositionRoot.Instance.Resolve<IUserServiceProxy>().Single(proxyContext, o.UserId);

            if (proxyContext.HasErrors)
                e.Result = proxyContext;
            else
                e.Result = user;
        }

        private void LoadDataRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ }), DispatcherPriority.ApplicationIdle);

            if (e.Result is ProxyContext)
            {
                (e.Result as ProxyContext).ShowErrors(Window.GetWindow(Parent));
                return;
            }

            var result = e.Result as UserUIO;

            if (result == null)
                return;

            if (result.Photo == null)
                result.Photo = PeplexUtils.ConvertBitmapImageToByteArray(new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "..\\..", "Resources\\DefaultProfile.png"))));

            PeplexConfig.Instance.CurrentUser = result;

            var child = new MainWindow { Owner = this };
            child.Show();
        }
        #endregion

        #region Password
        private void PasswordClick(object sender, MouseButtonEventArgs e)
        {
            var user = _users.FirstOrDefault(u => u.NickName.Equals(TxtUser.Text, StringComparison.CurrentCultureIgnoreCase));

            //Si existe el usuario
            if (user != null)
                SendEmail(user);
            else
                MessageBox.Show("El usuario introducido no existe", "AVISO", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void SendEmail(UserUIO user)
        {
            _wc = new WaitCursor();
            GInfo.IsEnabled = false;

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

            var newPassword = Membership.GeneratePassword(8, 1);
            var body = "<h2>Contraseña de PePlex reestablecida</h2><br>Apodo: " + o.User.NickName + "<br>Password: " + newPassword;
            var subject = "Peplex, petición reestablecimiento de contraseña";

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

                o.User.Password = newPassword;

                if (o.User.Photo == null)
                {
                    var image = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "..\\..", "Resources\\DefaultProfile.png")));

                    byte[] data;
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                        data = ms.ToArray();
                    }

                    o.User.Photo = data;
                }

                CompositionRoot.Instance.Resolve<IUserServiceProxy>().Update(new ProxyContext(), o.User);

                e.Result = true;
            }
            catch (Exception)
            {
                e.Result = false;
            }
        }

        private void SendEmailRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ GInfo.IsEnabled = true; }), DispatcherPriority.ApplicationIdle);

            var result = (bool)e.Result;

            if (result)
                MessageBox.Show("En unos segundos le llegará a su correo la nueva contraseña", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No se envio el correo correctamente", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region Events
        private void LoggingWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void KeyUpTextBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && GInfo.IsEnabled)
            {
                if (sender.GetType().Name == "TextBox")
                    TxtPass.Focus();
                else
                    BtnOk.Focus();
            }
        }
        #endregion
    }
}
