using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterWork_2.Graph
{
    internal class Edge<T>
    {
        public Vertex<T> Source;
        public Vertex<T> Destination;

        public Edge(Vertex<T> source, Vertex<T> destination)
        {
            Source = source;
            Destination = destination;
        }
    }
    internal class WeightedEdge<T> : Edge<T>
    {
        public int Weight;
        public WeightedEdge(Vertex<T> source, Vertex<T> destination, int weigth) : base(source, destination)
        {
            Weight = weigth;
            Source = source;
            Destination = destination;
        }
    }
}
