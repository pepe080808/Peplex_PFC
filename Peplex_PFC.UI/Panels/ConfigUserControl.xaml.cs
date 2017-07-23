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
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Panels
{
    public partial class ConfigUserControl
    {
        private List<UserUIO> _users;

        public List<UserUIO> Users
        {
            get { return _users; }
            set { _users = value;
                UpdateUI();
            }
        }

        private BitmapImage _currentBitmapImage;

        public ConfigUserControl()
        {
            InitializeComponent();
        }

        private void ConfigUserControlLoaded(object sender, RoutedEventArgs e)
        {
            if (PeplexConfig.Instance.CurrentUser.Permissions != 1)
                CbNickName.IsEnabled = CbPermissions.IsEnabled = BtnAdd.IsEnabled = BtnDelete.IsEnabled = false;
        }

        private void UpdateUI()
        {
            if (Users != null)
            {
                // Rellenamos el ComboBox con los NickName de los usuarios
                CbNickName.ItemsSource = Users;
                CbNickName.DisplayMemberPath = "NickName";

                var index = Users.FindIndex(u => u.NickName == PeplexConfig.Instance.CurrentUser.NickName);
                CbNickName.SelectedIndex = index;

                AssignData();
            }
        }
 

       private void CbNickNameSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AssignData();
        }

        private void AssignData()
        {
            TxtName.Text = Users[CbNickName.SelectedIndex].Name;
            TxtEmail.Text = Users[CbNickName.SelectedIndex].Email;
            CbPermissions.SelectedIndex = Users[CbNickName.SelectedIndex].Permissions;
            ImgProfile.Source = PeplexUtils.ConvertByteArrayToBitmapImage(Users[CbNickName.SelectedIndex].Photo);
            _currentBitmapImage = PeplexUtils.ConvertByteArrayToBitmapImage(Users[CbNickName.SelectedIndex].Photo);
        }

        private void BntAddClick(object sender, RoutedEventArgs e)
        {
            var child = new ConfigUserAddWindow {Owner = Window.GetWindow(Parent)};
            var result = child.ShowDialog();

            if (result == true)
            {
                Users.Add(child.NewUser);
                var index = Users.FindIndex(u => u.NickName == child.NewUser.NickName);
                CbNickName.SelectedIndex = index;
            }
        }

        private void BntEditClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtCurrentPass.Text) && !string.IsNullOrWhiteSpace(TxtNewPass.Text))
            {
                // Comprobamos que la contraseña actual es correcta y así permitir que la actualizace
                if(Users[CbNickName.SelectedIndex].Password.Equals(TxtCurrentPass.Text))
                    UpdateData(TxtNewPass.Text);
                else
                    MessageBox.Show("La contraseña actual es incorrecta. No se actualizarán los datos del usuario.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrWhiteSpace(TxtCurrentPass.Text) && string.IsNullOrWhiteSpace(TxtNewPass.Text))
                    UpdateData("");
            else
                MessageBox.Show("Los dos campos para la contraseña deben estar rellenos si desea modificarla", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void UpdateData(string newPassword)
        {
            var editedUser = new UserUIO
            {
                Id = Users[CbNickName.SelectedIndex].Id,
                NickName = Users[CbNickName.SelectedIndex].NickName,
                Name = TxtEmail.Text,
                Email = TxtEmail.Text,
                Password = String.IsNullOrWhiteSpace(newPassword) ? Users[CbNickName.SelectedIndex].Password : newPassword,
                Permissions = CbPermissions.SelectedIndex,
                Photo = PeplexUtils.ConvertBitmapImageToByteArray(_currentBitmapImage)
            };

            CompositionRoot.Instance.Resolve<IUserServiceProxy>().Update(new ProxyContext(), editedUser);

            MessageBox.Show("Usuario actualizado con éxito.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BntDeleteClick(object sender, RoutedEventArgs e)
        {
            if(Users[CbNickName.SelectedIndex].NickName.Equals(PeplexConfig.Instance.CurrentUser.NickName, StringComparison.CurrentCultureIgnoreCase))
                MessageBox.Show("No se puede eliminar el usario con el que se ha iniciado sesión.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                Users.RemoveAt(CbNickName.SelectedIndex);

                var index = Users.FindIndex(u => u.NickName == PeplexConfig.Instance.CurrentUser.NickName);
                CbNickName.SelectedIndex = index;
            }
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
                
                ImgProfile.Source = bitmapImage;
                _currentBitmapImage = bitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nEl archivo seleccionado no es un tipo de imagen válido");
            }
        }

       
    }
}
