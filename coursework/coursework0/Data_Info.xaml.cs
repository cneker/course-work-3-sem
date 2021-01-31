using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;

namespace coursework0
{
    public partial class Data_Info : Window
    {

        public string FileName;

        bool up_del = true;

        public Data_Info()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            up_del = true;
            this.DialogResult = true;
        }

        public string Get_Name
        {
            get { return nameTextBox.Text; }
        }

        public string Get_Sur
        {
            get { return surTextBox.Text; }
        }

        public string Get_Pat
        {
            get { return patTextBox.Text; }
        }

        public string Get_Num
        {
            get { return numTexBox.Text; }
        }

        public string Get_Bth
        {
            get { return birthTexBox.Text; }
        }

        public string Get_Adr
        {
            get { return adrTexBox.Text; }
        }

        public string Update_Kaf
        {
            get { return updateKaf.Text; }
        }

        public string Update_Spec
        {
            get { return updateSpec.Text; }
        }

        public bool Up_Del
        {
            get { return up_del; }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            up_del = false;
            this.DialogResult = true;
        }

        private void Image_give_me(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
                "Image files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            ofdPicture.FilterIndex = 1;
            if (ofdPicture.ShowDialog() == true)
            {
                FileName = ofdPicture.FileName;
                Foto.Visibility = Visibility.Hidden;
                Foto_lower.Visibility = Visibility.Visible;
                imgImage.Source =
                    new BitmapImage(new System.Uri(FileName));
            }
        }
    }
}
