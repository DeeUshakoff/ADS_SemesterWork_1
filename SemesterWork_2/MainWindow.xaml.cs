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
using System.IO;
using Microsoft.Win32;
using SemesterWork_2.Filework;
using DeeULib;

namespace SemesterWork_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.MainWindow = this;
            InitializeComponent();
            //ResizeMode = ResizeMode.NoResize;
            //GenerateTreeButton.IsEnabled = false;
        }
        public static void SetVisibility(Visibility visibility)
        {
           
        }
        private void GenerateTree_OnClick(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = "generating...";
            

            var gw = new GenerationWindow();
            
            gw.Show();
            //this.ShowDialog();
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider) sender).SelectionEnd = (int)e.NewValue;
           
            currentValue.Content = "Pack's max length: " + (int)((Slider)sender).SelectionEnd;
        }


        private void PackLengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = (int)e.NewValue;

            PackValueLabel.Content = "Pack's count: " + (int)((Slider)sender).SelectionEnd;
        }

        private void SetPath_Click(object sender, RoutedEventArgs e)
        {
            var ofd = SelectSaveDirectory();

            if (Directory.Exists(ofd.SelectedPath))
            {
                PathLabel.Content = ofd.SelectedPath;
                GenerateTreeButton.IsEnabled = true;

                File.Create(ofd.SelectedPath + (FileNameTextBox.Text == "enter file name" ? "/data.tree" : $"/{FileNameTextBox.Text}.tree"));
            }
            else
            {
                if (Directory.Exists(PathLabel.Content.ToString()))
                    return;
                GenerateTreeButton.IsEnabled = false;
            }

        }
        public static System.Windows.Forms.FolderBrowserDialog SelectSaveDirectory()
        {
            
            var ofd = new System.Windows.Forms.FolderBrowserDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
            };
            ofd.ShowDialog();

            return ofd;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "tree file (*.tree)|*.tree",
                Title = "Select graph to open",
                DefaultExt = ".tree"
            };

            
            ofd.ShowDialog();
            if (string.IsNullOrWhiteSpace(ofd.FileName))
                return;
            try
            {
                var sr = new StreamReader(ofd.FileName);
                //MessageBox.Show("OK");

                var first = double.Parse(sr.ReadLine());
                if (first % App.APP_ID != 0)
                {
                    sr.Dispose();
                    throw new Exception("Unable to read file");
                }
                

                List<int[]> Data = new();
                foreach (var el in Enumerable.Range(1, sr.ReadLine().ToInt()))
                {  Data.Add(Array.ConvertAll(sr.ReadLine().Split('|'), int.Parse)); 
                }

                AsMessage(String.Join(' ', Data[0]));
                sr.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

                return;
            }
            
        }
        public static void AsMessage(object input) => MessageBox.Show(input.ToString());
    }
}