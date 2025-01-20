using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaxPotoc
{
    /// <summary>
    /// Логика взаимодействия для Diniza.xaml
    /// </summary>
    public partial class Diniza : Window
    {
        public Diniza()
        {
            InitializeComponent();
        }

        private void CalculateMaxFlow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Чтение данных графа из текстового поля
                var input = GraphInput.Text;
                var graph = ParseGraph(input);
                int source = int.Parse(SourceInput.Text);
                int sink = int.Parse(SinkInput.Text);

                // Вычисление максимального потока
                var dinic = new DinicAlgorithm(graph, source, sink);
                int maxFlow = dinic.GetMaxFlow();

                // Вывод результата
                ResultOutput.Text = $"Максимальный поток: {maxFlow}";
            }
            catch (Exception ex)
            {
                ResultOutput.Text = $"Ошибка: {ex.Message}";
            }
        }
        private List<(int, int)>[] ParseGraph(string input)
        {
            var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int n = int.Parse(lines[0]);
            var graph = new List<(int, int)>[n];

            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<(int, int)>();
            }

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split();
                if (parts.Length != 3) throw new FormatException("Каждая строка должна содержать 3 числа: u, v, capacity.");

                int u = int.Parse(parts[0]);
                int v = int.Parse(parts[1]);
                int capacity = int.Parse(parts[2]);

                if (u < 0 || u >= n || v < 0 || v >= n)
                    throw new ArgumentException($"Ребро ({u}, {v}) ссылается на несуществующую вершину.");

                graph[u].Add((v, capacity));
                graph[v].Add((u, 0)); // Обратное ребро с нулевой пропускной способностью
            }

            return graph;
        }
    }
}
