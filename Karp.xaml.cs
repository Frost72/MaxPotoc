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
    /// Логика взаимодействия для Karzanov.xaml
    /// </summary>
    public partial class Karp : Window
    {
        public Karp()
        {
            InitializeComponent();
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            // Входные данные для графа и начальная точка.
            int verticesCount = int.Parse(VerticesCountTextBox.Text);
            int[,] graph = new int[verticesCount, verticesCount];

            // Заполняем граф с помощью пользовательского ввода.
            string[] edgeWeights = GraphInputTextBox.Text.Split(';');
            for (int i = 0; i < verticesCount; i++)
            {
                string[] weights = edgeWeights[i].Split(',');
                for (int j = 0; j < verticesCount; j++)
                {
                    graph[i, j] = int.Parse(weights[j]);
                }
            }

            // Используем алгоритм Эдмондса-Карпа для нахождения максимального потока
            KarpAlgorithm calculator = new KarpAlgorithm();

            int maxFlow = calculator.MaxFlow(graph,0, verticesCount - 1);

            ResultTextBox.Text = $"Максимальный поток: {maxFlow}";
        }
    }
}
