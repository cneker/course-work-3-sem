using System.Windows;

namespace coursework0
{
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Get_Name
        {
            get { return nameTextBox.Text; }
        }

        public string Update_Kaf
        {
            get { return updateKaf.Text; }
        }

        public string Update_Spec
        {
            get { return updateSpec.Text; }
        }
    }
}
