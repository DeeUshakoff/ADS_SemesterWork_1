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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

namespace SemesterWork_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            
        }

        private void GenerateTree_OnClick(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = "generating...";

            
            
            
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider) sender).SelectionEnd = (int)e.NewValue;
           
            currentValue.Content = "Pack's max length: " + (int)((Slider)sender).SelectionEnd;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PackLengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = (int)e.NewValue;

            PackValueLabel.Content = "Pack's count: " + (int)((Slider)sender).SelectionEnd;
        }

        private void SetPath_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.ShowDialog();
            PathLabel.Content = ofd.FileName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}