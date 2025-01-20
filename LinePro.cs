using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPotoc
{
    public class LinePro
    {
        public int FindMaxFlow(Graph graph)
        {
            // Здесь реализуйте решение задачи максимального потока методом ЛП.
            int n = graph.VertexCount;
            int m = 0; // Количество ребер
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (graph.Capacity[i, j] > 0) m++;

            // Формируем симплекс-таблицу
            int variableCount = m; // Количество переменных (ребра)
            int constraintCount = 2 * m + n; // Количество ограничений (ребра + вершины)

            double[,] tableau = new double[constraintCount + 1, variableCount + 1];
            int[,] edgeIndex = new int[n, n];
            int index = 0;

            // Заполняем таблицу для ребер
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (graph.Capacity[i, j] > 0)
                    {
                        edgeIndex[i, j] = index++;
                    }
                }
            }

            // Ограничения на поток по каждому ребру
            index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (graph.Capacity[i, j] > 0)
                    {
                        tableau[index, edgeIndex[i, j]] = 1;
                        tableau[index, variableCount] = graph.Capacity[i, j];
                        index++;
                    }
                }
            }

            // Ограничения на сохранение потока (вход = выход)
            for (int v = 0; v < n; v++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (graph.Capacity[i, v] > 0) tableau[index, edgeIndex[i, v]] = 1;
                }
                for (int j = 0; j < n; j++)
                {
                    if (graph.Capacity[v, j] > 0) tableau[index, edgeIndex[v, j]] = -1;
                }
                index++;
            }

            // Целевая функция: Максимизация потока через сток
            for (int i = 0; i < n; i++)
            {
                if (graph.Capacity[i, graph.Sink] > 0)
                {
                    tableau[constraintCount, edgeIndex[i, graph.Sink]] = 1;
                }
            }

            // Решаем симплекс-методом
            double[] result = SimplexMethod(tableau);
            return (int)result.Last(); // Возвращаем значение целевой функции
        }

        private double[] SimplexMethod(double[,] tableau)
        {
            int rows = tableau.GetLength(0);
            int cols = tableau.GetLength(1);
            while (true)
            {
                // Выбираем ведущий столбец
                int pivotColumn = -1;
                for (int j = 0; j < cols - 1; j++)
                {
                    if (tableau[rows - 1, j] > 0)
                    {
                        pivotColumn = j;
                        break;
                    }
                }
                if (pivotColumn == -1) break; // Оптимальное решение найдено

                // Выбираем ведущую строку
                int pivotRow = -1;
                double minRatio = double.MaxValue;
                for (int i = 0; i < rows - 1; i++)
                {
                    if (tableau[i, pivotColumn] > 0)
                    {
                        double ratio = tableau[i, cols - 1] / tableau[i, pivotColumn];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            pivotRow = i;
                        }
                    }
                }
                if (pivotRow == -1) throw new InvalidOperationException("Задача неразрешима");

                // Приводим симплекс-таблицу к новому базису
                double pivotValue = tableau[pivotRow, pivotColumn];
                for (int j = 0; j < cols; j++)
                {
                    tableau[pivotRow, j] /= pivotValue;
                }
                for (int i = 0; i < rows; i++)
                {
                    if (i == pivotRow) continue;
                    double multiplier = tableau[i, pivotColumn];
                    for (int j = 0; j < cols; j++)
                    {
                        tableau[i, j] -= multiplier * tableau[pivotRow, j];
                    }
                }
            }

            // Возвращаем результат
            double[] result = new double[cols - 1];
            for (int j = 0; j < cols - 1; j++)
            {
                for (int i = 0; i < rows - 1; i++)
                {
                    if (tableau[i, j] == 1)
                    {
                        result[j] = tableau[i, cols - 1];
                        break;
                    }
                }
            }
            result[result.Length - 1] = tableau[rows - 1, cols - 1]; // Значение целевой функции
            return result;

            throw new NotImplementedException("Метод ЛП для максимального потока пока не реализован.");
        }
    }
}
