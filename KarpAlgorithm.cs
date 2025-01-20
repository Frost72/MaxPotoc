using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPotoc
{
    public class KarpAlgorithm
    {

        private int BFS(int[,] residualGraph, int source, int sink, int[] parent)
        {
            int vertices = residualGraph.GetLength(0);
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
                    if (!visited[v] && residualGraph[u, v] > 0)
                    {
                        queue.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;

                        if (v == sink)
                            return 1; // Минимальный путь найден
                    }
                }
            }
            return 0; // Пути не найдено
        }

        public int MaxFlow(int[,] graph, int source, int sink)
        {
            int u, v;
            int vertices = graph.GetLength(0);
          
            // Создаем остаточную сеть и копируем в нее исходную сеть
            int[,] residualGraph = new int[vertices, vertices];
            for (u = 0; u < vertices; u++)
                for (v = 0; v < vertices; v++)
                    residualGraph[u, v] = graph[u, v];

            int[] parent = new int[vertices];
            int maxFlow = 0;

            // Пока существует путь из истока в сток
            while (BFS(residualGraph, source, sink, parent) > 0)
            {
                // Определяем минимальную пропускную способность пути
                int pathFlow = int.MaxValue;
                for (v = sink; v != source; v = parent[v])
                {
                    u = parent[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                }

                // Обновляем остаточную сеть
                for (v = sink; v != source; v = parent[v])
                {
                    u = parent[v];
                    residualGraph[u, v] -= pathFlow;
                    residualGraph[v, u] += pathFlow;
                }

                // Увеличиваем общий поток
                maxFlow += pathFlow;
            }

            return maxFlow;
        }





    }
}
