﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaxPotoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Dinisa_Click(object sender, RoutedEventArgs e)
        {
            Diniza diniza = new Diniza();
            diniza.Show();
        }

        private void Karzanov_Click(object sender, RoutedEventArgs e)
        {
            Karzanov karzanov = new Karzanov();
            karzanov.Show();
        }

        private void Karp_Click(object sender, RoutedEventArgs e)
        {
            Karp karp = new Karp();
            karp.Show();
        }

        private void FordFulkersonButton_Click(object sender, RoutedEventArgs e)
        {
            Ford ford = new Ford();
            ford.Show();
        }

        private void LinearProgrammingButton_Click(object sender, RoutedEventArgs e)
        {
            Line line = new Line();
            line.Show();
        }
    }
}