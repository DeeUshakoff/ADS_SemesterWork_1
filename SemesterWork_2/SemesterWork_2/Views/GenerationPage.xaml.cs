using SemesterWork_2.Tree;
using SemesterWork_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
namespace SemesterWork_2.Views
{
    public sealed partial class GenerationPage : Page
    {
        public GenerationViewModel ViewModel { get; } = new GenerationViewModel();
        private string path;
        public Windows.Storage.StorageFolder StorageFolder { get; private set; }
        public GenerationPage()
        {
            InitializeComponent();

            Text_Path.Text = "Picked folder: Empty";
        }

        private async void Button_SetPath_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var fpicker = new FolderPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            fpicker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFolder folder = await fpicker.PickSingleFolderAsync();
            if (folder != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                path = System.IO.Path.Combine(folder.Path, !string.IsNullOrWhiteSpace(TextBox_FileName.Text) ? TextBox_FileName.Text + ".tree" : "segmentTree.tree");
                Text_Path.Text = "Picked folder: " + path;
                StorageFolder = folder;
                Button_Generate.IsEnabled = true;
            }
        }

        private async void Button_Open_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            picker.FileTypeFilter.Add(".tree");

            var file = await picker.PickSingleFileAsync();
            var fwork = new Filework.SM2_Filework();

            if (file == null)
            {
                DisplayAsContentDialog("Empty file, select another file");
                return;
            }
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            await fwork.OpenAsync(file);
            timer.Stop();
            ShowDataDialog(fwork.Data, timer, "File opened");
        }

        private void Button_Generate_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            var fwork = new Filework.SM2_Filework();
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            fwork.Generate(path, (int)Slider_PacksCount.Value, (int)Slider_PacksSize.Value);
            timer.Stop();
            var res = fwork.Data;

            ShowDataDialog(res, timer, "File generated");
        }
        private async void ShowDataDialog(List<List<int>> data, System.Diagnostics.Stopwatch timer, string title)
        {
            if (data == null || data.SelectMany(x => x).Count() == 0)
            {
                DisplayAsContentDialog("Empty or wrong data, select another file");
                return;
            }

            var tree = new TreeView();
            tree.RootNodes.Add(new TreeViewNode() { Content = $"Packs {data.Count}" });
            tree.CanDragItems = false;


            var button_visualize = new Button() { Content = "Show hierarchically" };
            var preContent = new StackPanel
            {
                Spacing = 5
            };
            preContent.Children.Add(new TextBlock() { Text = $"{timer.Elapsed.TotalSeconds} sec spent" });
            preContent.Children.Add(button_visualize);
            var dialog_ready = new ContentDialog()
            {
                Content = preContent,
                Title = title,
                CloseButtonText = "Close",
                PrimaryButtonText = "Open",
                SecondaryButtonText = "Save",
                DefaultButton = ContentDialogButton.Primary
            };
            button_visualize.Click += Button_visualize_Click;

            async void Button_visualize_Click(object send, Windows.UI.Xaml.RoutedEventArgs args)
            {
                preContent.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                dialog_ready.Content = new ProgressRing() { IsActive = true, Visibility = Windows.UI.Xaml.Visibility.Visible };
                dialog_ready.Title = "Visualization hierarchically...";
                await Task.Delay(1);
                Visualising();

            }

            var result = await dialog_ready.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var trees = new List<SegmentTree>(data.Count) { };

                timer.Reset();
                timer.Start();
                data.ForEach(x => trees.Add(new SegmentTree(x)));
                timer.Stop();
                new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                    .AddText(timer.Elapsed.TotalSeconds.ToString())
                    .Show();
            }
            else if (result == ContentDialogResult.Secondary)
            {
                var trees = new List<SegmentTree>(data.Count) { };

                timer.Reset();
                timer.Start();
                data.ForEach(x => trees.Add(new SegmentTree(x)));
                timer.Stop();
                new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                    .AddText(timer.Elapsed.TotalSeconds.ToString())
                    .Show();
                SaveTree(trees);
            }
            void Visualising()
            {
                foreach (var item in data)
                {
                    var node = new TreeViewNode();
                    foreach (var el in item)
                    {
                        var elNode = new TreeViewNode
                        {
                            Content = el.ToString()
                        };
                        node.Children.Add(elNode);
                    }

                    node.Content = $"Pack len: {item.Count}";
                    tree.RootNodes[0].Children.Add(node);
                }

                var content = new StackPanel();
                var scroll = new ScrollViewer
                {
                    Content = content
                };
                content.Children.Add(new TextBlock() { Text = $"    {timer.Elapsed.TotalSeconds} sec spent" });
                content.Children.Add(tree);

                dialog_ready.Content = scroll;
                dialog_ready.Title = title;

            }


        }


        public static void DisplayAsContentDialog(object message)
        {
            var dialog = new ContentDialog()
            {
                Content = message.ToString(),
                CloseButtonText = "Ok"
            };
            _ = dialog.ShowAsync();
        }

        private void Test(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var tree = new SegmentTree(new List<int>()
            {1,1,1,1,2,2,2,2,3,3,3,4,4,4,4,4,1,1,1,1,2,2,2,2,3,3,3,4,4,4,4,4,1,1,1,1,2,2,2,2,3,3,3,4,4,4,4,4,1,1,1,1,2,2,2,2,3,3,3,4,4,4,4,4,1,1,1,1,2,2,2,2,3,3,3,4,4,4,4,4,1,1,1,1,2,2,2,2,3,3,3,4,4,4,4,4});
            SaveTree(tree);
            //DisplayAsContentDialog(tree.ToString() + "\n" + tree.Count.ToString());
            //var Data = new List<double>() { 1, 2, 3, 4, 5, 6, 7, 8 };


            //DisplayAsContentDialog(s[1].ToList()[0]);
        }
        public async void SaveTree(SegmentTree tree)
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation =
                PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Tree", new List<string>() { ".tree" });
            savePicker.SuggestedFileName = "New tree";

            var file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                Filework.SM2_Filework.Save(tree, file);
                //Windows.Storage.Provider.FileUpdateStatus status =
                //    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync((Windows.Storage.IStorageFile)file);
            }
        }
        public void SaveTree(List<SegmentTree> trees)
        {
            
            if (StorageFolder != null)
            {
                Filework.SM2_Filework.Save(trees, StorageFolder);
                //Windows.Storage.Provider.FileUpdateStatus status =
                //    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync((Windows.Storage.IStorageFile)file);
            }
        }
        
        private void Button_AddValue_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!double.IsNaN(NumberBox_AddValue.Value))
            {
                TreeView_GenerationPage.RootNodes.Add(new Microsoft.UI.Xaml.Controls.TreeViewNode() { Content = NumberBox_AddValue.Value.ToString() });
                Button_CustomGenerate.IsEnabled = true;
            }
        }

        private void NumberBox_AddValue_ValueChanged(Microsoft.UI.Xaml.Controls.NumberBox sender, Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs args)
        {
            Button_AddValue.IsEnabled = !double.IsNaN(NumberBox_AddValue.Value);
        }

        private async void Button_CustomGenerate_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
           
            var list = TreeView_GenerationPage.RootNodes.Select(x => Convert.ToInt32(x.Content));
            var tree = new SegmentTree(list);
            var dialog = new ContentDialog()
            {
                Title = "Tree generated",
                Content = $"List elements, {list.Count()}\nTree vertexes: {tree.Count}\nTree sum: {tree.Head.Data}",
                CloseButtonText = "Close",
                PrimaryButtonText = "Open",
                SecondaryButtonText = "Save",
                DefaultButton = ContentDialogButton.Primary
            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
                SaveTree(tree);
        }
    }
}
