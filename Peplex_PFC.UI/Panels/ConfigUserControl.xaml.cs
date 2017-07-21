using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Proxies;
using Peplex_PFC.UIO;
using Utils;

namespace Peplex_PFC.UI.Panels
{
    public partial class ConfigUserControl
    {
        private WaitCursor _wc;

        public ConfigUserControl()
        {
            InitializeComponent();
        }


        private void ConfigUserControlLoaded(object sender, RoutedEventArgs e)
        {
            if (PeplexConfig.Instance.CurrentUser.Permissions != 1)
            {
                //Pestaña usuarios
                CbNickName.IsEnabled= false;
                CbPermissions.IsEnabled = false;
                //Botones
                BtnAdd.Visibility = Visibility.Collapsed;
                BtnDelete.Visibility = Visibility.Collapsed;

                //rellenamos los campos con del usuario del apodo actual
                CbNickName.Items.Add(PeplexConfig.Instance.CurrentUser.NickName);
                TxtName.Text = PeplexConfig.Instance.CurrentUser.Name;
                TxtEmail.Text = PeplexConfig.Instance.CurrentUser.Email;
                CbPermissions.SelectedIndex = PeplexConfig.Instance.CurrentUser.Permissions;
                ImgProfile.Source = PeplexUtils.ConvertByteArrayToBitmapImage(PeplexConfig.Instance.CurrentUser.Photo);
            }
            
        }

        private void BntOkClick(object sender, RoutedEventArgs e)
        {

        }

        private void BntAddClick(object sender, RoutedEventArgs e)
        {

        }

        #region Editar
        private void BntEditClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtCurrentPass.Text) && !string.IsNullOrWhiteSpace(TxtNewPass.Text))
                EditUser();
        }

        private void EditUser()
        {
            _wc = new WaitCursor();
            GMain.IsEnabled = false;

            var bw = new BackgroundWorker();
            bw.DoWork += EditUserOnDoWork;
            bw.RunWorkerCompleted += EditUserRunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void EditUserOnDoWork(object sender, DoWorkEventArgs e)
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

        private void EditUserRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

            Users.AddRange(result);
        }
        #endregion
        private void BntDeleteClick(object sender, RoutedEventArgs e)
        {
        }

        private void ImgProfileClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog { Filter = "PNG|*.png JPG|*jpg", Title = "Seleccíone imagen para el perfil", FilterIndex = 1};
                if (dialog.ShowDialog() != true)
                    return;

                string strImagen = dialog.FileName;
                var bitmapImage = new BitmapImage(new Uri(strImagen));

                PeplexConfig.Instance.CurrentUser.Photo = PeplexUtils.ConvertBitmapImageToByteArray(bitmapImage);
                ImgProfile.Source = PeplexUtils.ConvertByteArrayToBitmapImage(PeplexConfig.Instance.CurrentUser.Photo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nEl archivo seleccionado no es un tipo de imagen válido");
            }
        }
    }
}
