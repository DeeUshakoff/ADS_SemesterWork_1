using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;
using Windows.Storage;
namespace SemesterWork_2.Filework
{
    
    public interface IDataSet<T> { object Unpack(); }

    public class SM_2_DataSet : IDataSet<List<List<int>>>
    {
        readonly List<List<int>> Data;
        public SM_2_DataSet(List<List<int>> data) => Data = data;
        public object Unpack() => Data;
    }
    public class SM2_Filework 
    {
        public List<List<int>> Data { get; private set; }
       
        public void Generate(string path, int maxPacksCount = 20, int maxPackSize = 100)
        {
            var localData = new List<List<int>>();
            foreach (var pack_i in Enumerable.Range(0, new Random().Next(20, maxPacksCount)))
            {
                var pack = new List<int> { };
                foreach (var el_i in Enumerable.Range(0, new Random().Next(100, maxPackSize)))
                {
                    pack.Add(new Random().Next(0, 100));
                }
                localData.Add(pack);
            }
            Data = localData;
        }

        public async Task OpenAsync(StorageFile file)
        {
            try
            {
                Data = (await FileIO.ReadLinesAsync(file)).Select(x => Array.ConvertAll(x.Split(' '), int.Parse).ToList()).ToList();
            }
            catch
            {
                Data = null;
            }       
        }  

        public static async void Save(Tree.SegmentTree tree, StorageFile file)
        {
            CachedFileManager.DeferUpdates(file);

            var lines = new List<string>(tree.Data.Count) { };
            foreach (var line in tree.Data)
                lines.Add(string.Join(" ", line.Select(x => x.Data)));
            await Windows.Storage.FileIO.WriteLinesAsync(file, lines);
        }
        public static async void Save(List<Tree.SegmentTree> trees, StorageFolder folder)
        {

            var lines = new List<string>(trees.Count) { };
            foreach (var tree in trees)
                lines.Add(string.Join(" ", tree.Data.Last().Select(x => x.Data)) + tree.Data.First().First().ToString());

       
            foreach (var el in lines)
            {
                var file = await folder.CreateFileAsync("tree", CreationCollisionOption.GenerateUniqueName);

                await FileIO.WriteLinesAsync(file, new List<string>() { el });
            }
        }
    }
}
