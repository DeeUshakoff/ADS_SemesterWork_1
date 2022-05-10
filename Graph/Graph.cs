using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterWork_2.Graph
{
    internal class Graph<T> : SemesterWork_2.Filework.IDataSet<T>
    {
        //public List<Vertex<T>>? Vertices;
        public Vertex<T>? HeadVertex;

        public object Unpack()
        {
            throw new NotImplementedException();
        }
    }
}
