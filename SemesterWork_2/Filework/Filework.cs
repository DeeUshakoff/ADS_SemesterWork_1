using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterWork.Extensions;
using DeeULib;
namespace SemesterWork_2.Filework
{
    public interface IFilework<T>
    {
        public static void Generate(string path, object data) { }
        public static void Save(IDataSet<T> file) { }
        public IDataSet<T>? Open(string path);
    }
    public interface IDataSet<T> { public object Unpack(); }

    internal class SM2_Filework<T> : IFilework<T>
    {
        IDataSet<T> Data;
        public SM2_Filework(string path) => Data = Open(path);

        public static void Generate(string path, object data)
        {
            throw new NotImplementedException();
        }

        public IDataSet<T> Open(string path)
        {
            var sr = new StreamReader(path);
            
            if(sr.ReadLine().ToDouble() % App.APP_ID != 0)
            {
                
            }
            var Data = sr.ReadToEnd();
            //var Data = File.ReadAllLines(path).Select(x => Array.ConvertAll(x.Split(' '), int.Parse)).ToList();
            throw new Exception("");
        }  

        public static void Save(IDataSet<T> file)
        {
            throw new NotImplementedException();
        }
    }
}
