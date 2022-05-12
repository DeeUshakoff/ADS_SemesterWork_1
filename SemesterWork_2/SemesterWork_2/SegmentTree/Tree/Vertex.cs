using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterWork_2.Tree
{
    public class Vertex<T>
    {
        public int ID;
        public T Data;
        public Vertex<T> Parent;
        public Vertex<T> LeftChild;
        public Vertex<T> RightChild;

        public Vertex(int id, T data, Vertex<T> leftChild = null, Vertex<T> rightChild = null, Vertex<T> parent = null)
        {
            Parent = parent;
            LeftChild = leftChild;
            RightChild = rightChild;
            ID = id;
            Data = data;

          
        }
        public bool IsExternal() => (LeftChild == null && RightChild == null);
        public override string ToString() => $"\nid: {ID}, data: {Data}, (leftChild: id - {LeftChild.ID}, data - {LeftChild.Data}) | (rightChild: id - {RightChild.ID}, data - {RightChild.Data})";
       
    }
}
