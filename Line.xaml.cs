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
    /// Логика взаимодействия для Line.xaml
    /// </summary>
    public partial class Line : Window
    {
        public Line()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Парсинг ввода и создание графа
                var graph = ParseGraphInput(GraphInput.Text);

                // Вычисление максимального потока
                var lp = new LinePro();
                int maxFlow = lp.FindMaxFlow(graph);

                // Вывод результата
                ResultOutput.Text = $"Максимальный поток: {maxFlow}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private Graph ParseGraphInput(string input)
        {
            var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var edges = new List<(int, int, int)>();
            int maxVertex = 0;

            foreach (var line in lines)
            {
                var parts = line.Split('-');
                int from = int.Parse(parts[0]);
                int to = int.Parse(parts[1]);
                int capacity = int.Parse(parts[2]);
                edges.Add((from, to, capacity));
                maxVertex = Math.Max(maxVertex, Math.Max(from, to));
            }

            var graph = new Graph(maxVertex + 1);
            foreach (var (from, to, capacity) in edges)
            {
                graph.AddEdge(from, to, capacity);
            }

            graph.Source = 0; // Установите источник (по умолчанию 0).
            graph.Sink = maxVertex; // Установите сток (по умолчанию максимальная вершина).
            return graph;
        }

    }
}
