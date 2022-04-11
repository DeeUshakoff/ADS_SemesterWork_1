using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SemesterWork_2
{
    /// <summary>
    /// Логика взаимодействия для AddNode.xaml
    /// </summary>
    public partial class AddNodeWindow : Window
    {
        public double Result = Double.NaN;
        public AddNodeWindow()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) => Close();

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //InputField.Text = InputField.Text.Replace('.', ',');
                Result = Convert.ToDouble(InputField.Text);
                Close();
            }
            catch
            {
                MainWindow.AsMessage("Please, write number");
            }
        }
    }
}
