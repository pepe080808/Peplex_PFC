using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Peplex_PFC.UI.Config;
using Peplex_PFC.UI.Interfaces;
using Peplex_PFC.UI.Shared;
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

            var auxPhoto = Users[CbNickName.SelectedIndex].Photo;
            if (auxPhoto == null)
                auxPhoto = PeplexUtils.ConvertBitmapImageToByteArray(new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "..\\..", "Resources\\DefaultProfile.png"))));
            ImgProfile.Source = PeplexUtils.ConvertByteArrayToBitmapImage(auxPhoto);
            _currentBitmapImage = PeplexUtils.ConvertByteArrayToBitmapImage(auxPhoto);
        }

        private void BntAddClick(object sender, RoutedEventArgs e)
        {
            var child = new ConfigUserAddWindow {Owner = Window.GetWindow(Parent)};
            var result = child.ShowDialog();

            if (result == true)
            {
                Users.Add(child.NewUser);
                CbNickName.ItemsSource = null;
                CbNickName.ItemsSource = Users;
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
                    MessageBoxWindow.Show(Window.GetWindow(Parent), Translations.lblWarning, DialogIcon.Warning, new[] { DialogButton.Accept }, Translations.ConfigUserControlCurrentPasswordError);
            }
            else if (string.IsNullOrWhiteSpace(TxtCurrentPass.Text) && string.IsNullOrWhiteSpace(TxtNewPass.Text))
                    UpdateData("");
            else
                MessageBoxWindow.Show(Window.GetWindow(Parent), Translations.lblWarning, DialogIcon.Warning, new[] { DialogButton.Accept }, Translations.ConfigUserControlObligatoryBothPasswordField);
        }

        private void UpdateData(string newPassword)
        {
            if (Validate().Any())
            {
                MessageBoxWindow.Show(Window.GetWindow(Parent), Translations.lblWarning, DialogIcon.Warning, new[] { DialogButton.Accept }, String.Format(Translations.ConfigUserControlObligatoryField, String.Join(",", Validate())));
                return;
            }

            var editedUser = new UserUIO
            {
                Id = Users[CbNickName.SelectedIndex].Id,
                NickName = Users[CbNickName.SelectedIndex].NickName,
                Name = TxtName.Text,
                Email = TxtEmail.Text,
                Password = String.IsNullOrWhiteSpace(newPassword) ? Users[CbNickName.SelectedIndex].Password : newPassword,
                Permissions = CbPermissions.SelectedIndex,
                Photo = PeplexUtils.ConvertBitmapImageToByteArray(_currentBitmapImage)
            };

            var result = CompositionRoot.Instance.Resolve<IUserServiceProxy>().Update(editedUser);

            if (result)
            {
                // Si hemos actualizado el usuario actual, modificamos su foto de perfil
                if (PeplexConfig.Instance.CurrentUser.Id == editedUser.Id)
                    PeplexConfig.Instance.CurrentUser.Photo = editedUser.Photo;

                Users[CbNickName.SelectedIndex] = editedUser;

                MessageBoxWindow.Show(Window.GetWindow(Parent), Translations.lblInfo, DialogIcon.Info, new[] {DialogButton.Accept}, Translations.cUserUpdateSuccessfully);
            }
        }

        private List<string> Validate()
        {
            var result = new List<string>();

            if (String.IsNullOrWhiteSpace(TxtName.Text))
                result.Add("Nombre");
            if (String.IsNullOrWhiteSpace(TxtEmail.Text))
                result.Add("Email");

            return result;
        }

        private void BntDeleteClick(object sender, RoutedEventArgs e)
        {
            if(Users[CbNickName.SelectedIndex].NickName.Equals(PeplexConfig.Instance.CurrentUser.NickName, StringComparison.CurrentCultureIgnoreCase))
                MessageBoxWindow.Show(Window.GetWindow(Parent), Translations.lblWarning, DialogIcon.Warning, new[] { DialogButton.Accept }, Translations.ConfigUserControlDelteUserError);
            else
            {
                CompositionRoot.Instance.Resolve<IUserServiceProxy>().Delete(Users[CbNickName.SelectedIndex].Id);
                Users.RemoveAt(CbNickName.SelectedIndex);
                //CbNickName.Items.RemoveAt(CbNickName.SelectedIndex  );
                //var index = Users.FindIndex(u => u.NickName == PeplexConfig.Instance.CurrentUser.NickName);
                //CbNickName.SelectedIndex = index;
                UpdateUI();
            }
        }

        private void ImgProfileClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog { Filter = "PNG|*.png|JPG|*jpg", Title = Translations.ConfigUserControlSelectImageToProfile, FilterIndex = 1};
                if (dialog.ShowDialog() != true)
                    return;

                string strImagen = dialog.FileName;
                var bitmapImage = new BitmapImage(new Uri(strImagen));
                
                ImgProfile.Source = bitmapImage;
                _currentBitmapImage = bitmapImage;
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Show(Window.GetWindow(Parent), Translations.lblError, DialogIcon.CommError, new[] { DialogButton.Accept }, ex.Message + Translations.ConfigControlImageNotValidError);
            }
        }

       
    }
}
