using System.Windows;
using System.Windows.Controls;

namespace coursework0
{
    public partial class Search : Window
    {

        public string search_name = null;

        public Search()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e) //Проверяет на наличие введённых данных
        {
            if (search_name == null)
            {
                MessageBox.Show("Выберите критерий поиска!");
            }
            else if (nameTextBox.Text == "")
            {
                MessageBox.Show("Введите данные!");
            }
            else
            {
                this.DialogResult = true;
            }
        }

        public string Get_Text
        {
            get { return nameTextBox.Text; }
        }

        private void Insert_SearchValue(object sender, RoutedEventArgs e)
        {
            var radiobutton = sender as RadioButton;
            string bottonPressed = radiobutton.Content.ToString();
            search_name = bottonPressed;
        }
    }
}
