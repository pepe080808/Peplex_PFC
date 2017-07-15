using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Peplex_PFC.UI.Config;

namespace Peplex_PFC.UI.Panels
{
    public partial class ConfigUserControl
    {

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

                var photo = PeplexConfig.Instance.CurrentUser.Photo;
                if (photo != null)
                {
                    var memStream = new MemoryStream();
                    memStream.Write(photo, 0, photo.Length);
                    memStream.Seek(0, SeekOrigin.Begin);

                    var img = new BitmapImage();
                    img.BeginInit();
                    img.StreamSource = memStream;
                    img.EndInit();

                    ImgProfile.Source = img;
                }
                else
                    ImgProfile.Source = null;
            }
            
        }

        private void BntOkClick(object sender, RoutedEventArgs e)
        {

        }

        private void BntAddClick(object sender, RoutedEventArgs e)
        {

        }

        private void BntEditClick(object sender, RoutedEventArgs e)
        {

        }

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

                var image = new BitmapImage(new Uri(strImagen));

                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }

                PeplexConfig .Instance.CurrentUser.Photo = data;

                var photo = PeplexConfig.Instance.CurrentUser.Photo;
                if (photo != null)
                {
                    var memStream = new MemoryStream();
                    memStream.Write(photo, 0, photo.Length);
                    memStream.Seek(0, SeekOrigin.Begin);

                    var img = new BitmapImage();
                    img.BeginInit();
                    img.StreamSource = memStream;
                    img.EndInit();

                    ImgProfile.Source = img;
                }
                else
                    ImgProfile.Source = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "\nEl archivo seleccionado no es un tipo de imagen válido");
            }
        }
    }
}
