using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPotoc
{
    public class Graph
    {
        public int[,] Capacity { get; set; }
        public int Source { get; set; }
        public int Sink { get; set; }
        public int VertexCount { get; set; }

        public Graph(int vertexCount)
        {
            VertexCount = vertexCount;
            Capacity = new int[vertexCount, vertexCount];
        }

        public void AddEdge(int from, int to, int capacity)
        {
            Capacity[from, to] = capacity;
        }
    }
}
