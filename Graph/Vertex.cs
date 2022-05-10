using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterWork_2.Graph
{
    internal class Vertex<T>
    {
        public int ID;
        public T Data;
        public List<Vertex<T>>? Previous;
        public List<Vertex<T>>? Next;

        public Vertex(int id, T data, List<Vertex<T>>? previous = null, List<Vertex<T>>? next = null)
        {
            Previous = previous;
            Next = next;
            ID = id;
            Data = data;
        }
        public bool IsExternal() => (Previous == null && Next == null);
        
    }
}
