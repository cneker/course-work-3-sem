using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;

namespace coursework0
{
    public partial class Insert : Window
    {
        public Insert()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e) //Проверяет на наличие введённых данных
        {
            if (nameTextBox.Text == "" || surTextBox.Text == "" || patTextBox.Text == ""){
                MessageBox.Show("Не все данные введены!");
            }
            else {
                this.DialogResult = true;
            }
        }

        public string Get_Kaf
        {
            get { return insertKaf.Text; }
        }

        public string Get_Spec
        {
            get { return insertSpec.Text; }
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

        public string Get_Birth
        {
            get { return birthTexBox.Text; }
        }

        public string Get_Adr
        {
            get { return adrTexBox.Text; }
        }

        public string FileName = "";

        public string Get_Img
        {
            get { return FileName; }
        }

        private void Image_give_me(object sender, RoutedEventArgs e) //Получаем путь изображения
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.png";
            ofdPicture.FilterIndex = 1;
            if (ofdPicture.ShowDialog() == true)
            {
                FileName = ofdPicture.FileName;
                Foto.Visibility = Visibility.Hidden;
                imgImage.Source =
                    new BitmapImage(new System.Uri(FileName));
            }
        }
    }
}
