using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Toolkit.Uwp.Notifications;
namespace SemesterWork_2.Tree
{
    public class SegmentTree
    {
        public List<List<Vertex<int>>> Data { get; private set; } = new List<List<Vertex<int>>>();
        public Vertex<int> Head { get; private set; }
        public SegmentTree(IEnumerable<int> input) => CreateTree(input);

        public void Add(params int[] input) => CreateTree(Data.First().Select(x => x.Data).Concat(input));
        public void Add(IEnumerable<int> input) => CreateTree(Data.First().Select(x => x.Data).Concat(input));
        public int Count => Data.SelectMany(x => x).Count();

        private void CreateTree(IEnumerable<int> input)
        {
            Data = new List<List<Vertex<int>>>() { };
            Data.Add(input.Select(number => new Vertex<int>(0, number)).ToList());

            var height = Math.Ceiling(Math.Log(input.Count()) / Math.Log(2));

            for (int i = 1; i <= height; i++)
            {
                var parts = Data.Last().Count % 2 == 0 ? Data.Last().Split(2).ToList() : Data.Last().SkipLast(1).Split(2).ToList();

                var currentLevel = new List<Vertex<int>>(parts.Count);

                foreach (var el in parts)
                {
                    currentLevel.Add(new Vertex<int>(i, el.Select(x => x.Data).Sum(), el.First(), el.Last()));
                    el.First().Parent = currentLevel.Last();
                    el.Last().Parent = currentLevel.Last();
                }

                if (Data.Last().Count % 2 != 0)
                {
                    var lastNode = Data.Last().Last();
                    var sideNode = new Vertex<int>(lastNode.ID + 1, lastNode.Data, null, lastNode);
                    lastNode.Parent = sideNode;
                 
                    currentLevel.Add(sideNode);
                }

                Data.Add(currentLevel);
            }
            Head = Data.Last().Last();
            Data.Reverse();
            GenerateIDs();
            
        }
        private void RegenerateTree() => CreateTree(Data.Last().Select(x => x.Data));

        public bool Contains(int data) => Data.SelectMany(x => x).Any(x => x.Data == data);
        public void RemoveAt(int index)
        {
            if(index < 0 || index >= Data.Last().Count)
            {
                new ToastContentBuilder()
                    .AddText($"Index {index} is out of range, use value from 0 to {Data.Last().Count}")
                    .AddButton("Ok", ToastActivationType.Foreground, "")
                    .Show();
                   
                return;
            }    
            Data.Last().RemoveAt(index);
            RegenerateTree();
        }
        private void GenerateIDs()
        {
            var i = 0;
            foreach(var level in Data)
            {
                foreach(var el in level)
                {
                    i++;
                    el.ID = i;
                }
            }
        }
        
        //public override string ToString() => String.Join(" ", Data.Select(x => x).SelectMany(x => x));
        ////public override string ToString() => Data[1][0].ToString();

    }

    
    public static class LinqExt
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int size)
        {
            for (var i = 0; i < (float)list.Count() / size; i++)
                yield return list.Skip(i * size).Take(size);
        }
    }
}
