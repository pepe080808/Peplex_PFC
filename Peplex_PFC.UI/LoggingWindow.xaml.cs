using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Shared;
using Peplex_PFC.UI.UIO;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI
{
    public partial class LoggingWindow
    {
        const int MAX_OPPORTUNITIES = 3;
        private int _oportunidades;

        private List<UserUIO> _users { get; set; }

        //private WaitIndicatorOnThread _bussy;
        private WaitCursor _wc;

        public LoggingWindow()
        {
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
            var users = CompositionRoot.Instance.Resolve<IUserServiceProxy>().FindAll().ToList();

            e.Result = users;
        }

        private void LoadDataUsersRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ GInfo.IsEnabled = true; TxtUser.Focus(); }), DispatcherPriority.ApplicationIdle);

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
                _oportunidades++;
                MessageBoxWindow.Show(this, Translations.lblWarning, DialogIcon.Warning, new[] { DialogButton.Accept }, String.Format(Translations.LoggingWindowUserAndPasswordNotValid, _oportunidades, MAX_OPPORTUNITIES));
                if (_oportunidades == MAX_OPPORTUNITIES)
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

            var user = CompositionRoot.Instance.Resolve<IUserServiceProxy>().Single(o.UserId);

            e.Result = user;
        }

        private void LoadDataRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { _wc.Dispose(); /*_bussy.Hide();*/ }), DispatcherPriority.ApplicationIdle);

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
            {
                user.Photo = PeplexUtils.ConvertBitmapImageToByteArray(new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "..\\..", "Resources\\DefaultProfile.png"))));
                SendEmail(user);
            }
            else
                MessageBoxWindow.Show(this, Translations.lblWarning, DialogIcon.Warning, new[] { DialogButton.Accept }, Translations.loggingWindowUserNotExits);
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

            var newPassword = PeplexUtils.GeneratePassword();
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

                CompositionRoot.Instance.Resolve<IUserServiceProxy>().Update(o.User);

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
                MessageBoxWindow.Show(this, Translations.lblInfo, DialogIcon.Info, new[] { DialogButton.Accept }, Translations.loggingWindowEmailSentSuccessful);
            else
                MessageBoxWindow.Show(this, Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, Translations.loggingWindowEmailSentError);
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
