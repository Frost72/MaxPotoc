using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPotoc
{
    public class FordFulkerson
    {
        private bool BFS(int[,] residualGraph, int source, int sink, int[] parent)
        {
            bool[] visited = new bool[residualGraph.GetLength(0)];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            visited[source] = true;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                for (int v = 0; v < residualGraph.GetLength(1); v++)
                {
                    if (!visited[v] && residualGraph[u, v] > 0)
                    {
                        queue.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                        if (v == sink) return true;
                    }
                }
            }
            return false;
        }

        public int FindMaxFlow(Graph graph)
        {
            int[,] residualGraph = (int[,])graph.Capacity.Clone();
            int[] parent = new int[graph.VertexCount];
            int maxFlow = 0;

            while (BFS(residualGraph, graph.Source, graph.Sink, parent))
            {
                int pathFlow = int.MaxValue;
                for (int v = graph.Sink; v != graph.Source; v = parent[v])
                {
                    int u = parent[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                }

                for (int v = graph.Sink; v != graph.Source; v = parent[v])
                {
                    int u = parent[v];
                    residualGraph[u, v] -= pathFlow;
                    residualGraph[v, u] += pathFlow;
                }

                maxFlow += pathFlow;
            }
            return maxFlow;
        }
    }
}
