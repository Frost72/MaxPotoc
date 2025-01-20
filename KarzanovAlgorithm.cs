using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPotoc
{
    public class KarzanovAlgorithm
    {
        private int[,] capacity;
        private int vertices;

        public KarzanovAlgorithm(int[,] graph)
        {
            vertices = graph.GetLength(0);
            capacity = graph;
        }

        public int FindMaxFlow(int source, int sink)
        {
            int maxFlow = 0;
            int[] parent = new int[vertices];

            while (BFS(source, sink, parent))
            {
                int pathFlow = int.MaxValue;
                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    pathFlow = Math.Min(pathFlow, capacity[u, v]);
                }

                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    capacity[u, v] -= pathFlow;
                    capacity[v, u] += pathFlow;
                }

                maxFlow += pathFlow;
            }

            return maxFlow;
        }

        private bool BFS(int source, int sink, int[] parent)
        {
            bool[] visited = new bool[vertices];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            visited[source] = true;
            parent[source] = -1;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                for (int v = 0; v < vertices; v++)
                {
                    if (!visited[v] && capacity[u, v] > 0)
                    {
                        queue.Enqueue(v);
                        visited[v] = true;
                        parent[v] = u;

                        if (v == sink)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
