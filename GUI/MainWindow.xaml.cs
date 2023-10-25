using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Algorithms;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _percent = 0;
        public int Percent
        {
            get { return _percent; }
            set { _percent = value; NotifyPropertyChange(nameof(Percent)); }
        }

        private List<string> _combos = new();
        public List<string> Combos
        {
            get { return _combos; }
            set { _combos = value; NotifyPropertyChange(nameof(Combos)); }

        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly List<string> sorts = new() { "loop" /*, "recursive" */};

        private async void StartSort(object sender, RoutedEventArgs e)
        {
            FiveWords fiveWords = new("beta");
            List<List<string>> combinations = new();
            Stopwatch timer = new();
            StartButton.IsEnabled = false;
            SelectSort.IsEnabled = false;

            string sort = (string)SelectSort.SelectedValue ?? sorts[0];

            if (sort == "loop")
            {
                PercentBar.Maximum = 100;
                // fiveWords.SortForLoops();
                fiveWords.Progress += (sender, e) => Percent = e;
                fiveWords.Combinations += (sender, e) => Combos = new (Combos) { e };
                await fiveWords.SortForLoops();
                // int amountOfThreads = 16; Parallel.For(0, amountOfThreads, i => combinations.AddRange(fiveWords.SortForLoops((double)i / amountOfThreads, (double)(i + 1) / amountOfThreads)));
                timer.Start();
            }
            if (sort == "recursive") { }

            // MessageBox.Show($"Starter programmet med {sort} algoritmen");
            // CombinationsTextBlock.Text = string.Join(" ", combinations);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SelectSort.ItemsSource = sorts;
            SelectSort.SelectedIndex = 0;
        }
    }
}
