using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPotoc
{
    public class DinicAlgorithm
    {
        private  int vertices;
        private  List<(int, int)>[] graph;
        private  int[,] capacity;
        private  int[] level;
        private  int[] ptr;

        private  int source;
        private  int sink;

        public DinicAlgorithm(List<(int, int)>[] graph, int source, int sink)
        {
            vertices = graph.Length;
            this.graph = graph;
            capacity = new int[vertices, vertices];
            level = new int[vertices];
            ptr = new int[vertices];
            this.source = source;
            this.sink = sink;
            foreach (var u in graph)
            {
                foreach (var (v, cap) in u)
                {
                    capacity[u.GetHashCode(), v] = cap; // Заполнение матрицы пропускных способностей
                }
            }
        }

        public int GetMaxFlow()
        {
            int flow = 0;

            while (Bfs())
            {
                Array.Fill(ptr, 0);
                int pushed;
                do
                {
                    pushed = Dfs(source, int.MaxValue);
                    flow += pushed;
                } while (pushed != 0);
            }

            return flow;
        }

        private bool Bfs()
        {
            Array.Fill(level, -1);
            level[source] = 0;

            var queue = new Queue<int>();
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                foreach (var (v, _) in graph[u])
                {
                    if (level[v] == -1 && capacity[u, v] > 0)
                    {
                        level[v] = level[u] + 1;
                        queue.Enqueue(v);
                    }
                }
            }

            return level[sink] != -1;
        }

        private int Dfs(int u, int flow)
        {
            if (u == sink) return flow;

            for (; ptr[u] < graph[u].Count; ptr[u]++)
            {
                var (v, _) = graph[u][ptr[u]];

                if (level[v] == level[u] + 1 && capacity[u, v] > 0)
                {
                    int pushed = Dfs(v, Math.Min(flow, capacity[u, v]));

                    if (pushed > 0)
                    {
                        capacity[u, v] -= pushed;
                        capacity[v, u] += pushed;
                        return pushed;
                    }
                }
            }

            return 0;
        }
    }
}
