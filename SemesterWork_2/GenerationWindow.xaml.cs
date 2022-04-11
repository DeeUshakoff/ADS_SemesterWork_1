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
using SemesterWork_2.Graph;
namespace SemesterWork_2
{
    /// <summary>
    /// Логика взаимодействия для GenerationWindows.xaml
    /// </summary>
    public partial class GenerationWindow : Window
    {

        public static void MoveNode(Button node, int x, int y)
        {
            var oldPosition = node.Margin;
            node.Margin = new Thickness(oldPosition.Left + x, oldPosition.Top + y, 0, 0);
        }
        public void DrawLine(Button from, Button to)
        {
            var line = new Line();
            line.X1 = from.Margin.Left + 30;
            line.Y1 = from.Margin.Top;

            line.X2 = to.Margin.Left + 30;
            line.Y2 = to.Margin.Top + 60;

            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = 4;
            Space.Children.Add(line);

            line.Uid = from.Margin.ToString();
            
            MainWindow.AsMessage($" {Space.Children[2].Uid}" );
           //MainWindow.AsMessage($"{from.Margin}" );
           
        }
        public GenerationWindow()
        {
            InitializeComponent();
        }


        private void NodeButton_Click(object sender, RoutedEventArgs e)
        {
             
            var s = (Button)sender;
            MoveNode(s, 100, 100);
            //var transform = new TranslateTransform(5, 5);
            //var trg = (TransformGroup)s.RenderTransform;
            DrawLine(s, Node1);

            //trg.Children.Add(transform);

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addNodeDialog = new AddNodeWindow();
            addNodeDialog.ShowDialog();

            var node = new Button();
            var vertex = new Vertex<double>((int)addNodeDialog.Result, addNodeDialog.Result);

            node.Content = vertex;
            node.Margin = new Thickness(250, 250, 0, 0);
            node.Click += NodeButton_Click;
            node.Height = 60;
            node.Width = 60;
            node.VerticalAlignment = VerticalAlignment.Top;
            node.HorizontalAlignment = HorizontalAlignment.Left;


            var context = new ContextMenu();
            context.Items.Add(new MenuItem() { Header = "Remove" });
            var item2 = (new MenuItem() { Header = "Change value" }); item2.Click += ChangeNode_Click;
            context.Items.Add(item2);
            context.Items.Add(new MenuItem() { Header = "Info" });

            node.ContextMenu = context;
            Space.Children.Add(node);

            /*
             <Button x:Name="Node" Content="point" Margin="273,281,0,0" Click="NodeButton_Click" Height="60" VerticalAlignment="Top" HorizontalAlignment="Left" Width="60" RenderTransformOrigin="0.5,0.5" Uid="{Binding Margin, ElementName=Node}">
                <Button.ContextMenu>
                    <ContextMenu>
                        <MenuItem Header="Remove"/>
                        <MenuItem Header="Change value" Click="ChangeNode_Click" />
                        <MenuItem Header="Info"/>
                    </ContextMenu>
                </Button.ContextMenu>
                <Button.RenderTransform>
                    <TransformGroup >
                        <ScaleTransform/>
                        <SkewTransform/>
                        <RotateTransform/>
                        <TranslateTransform  X="0" Y="0"/>
                    </TransformGroup>
                </Button.RenderTransform>
            </Button>
             */



        }

        private void ChangeNode_Click(object sender, RoutedEventArgs e)
        {
            var GetValueWindow = new AddNodeWindow();
            GetValueWindow.Title = "Change value";
            GetValueWindow.OkButton.Content = "Ok";
            GetValueWindow.ShowDialog();
            if (!double.IsNaN(GetValueWindow.Result))
            {
                var btn = ((MenuItem)sender).Parent;
                MainWindow.AsMessage(btn.ToString());
            }
        }
    }

   
}
