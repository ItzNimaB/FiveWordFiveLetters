using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;
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

        private int _maxPercent = 0;
        public int MaxPercent
        {
            get { return _maxPercent; }
            set { _maxPercent = value; NotifyPropertyChange(nameof(MaxPercent)); }
        }

        private List<string> _combos = new();
        public List<string> Combos
        {
            get { return _combos; }
            set { _combos = value; NotifyPropertyChange(nameof(Combos)); }
        }
        private int _combosCount = 0;
        public int CombosCount
        {
            get { return _combosCount; }
            set { _combosCount = value; NotifyPropertyChange(nameof(CombosCount));  }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly List<string> sorts = new() { "loop" /*, "recursive" */};
        public static readonly List<string> files = new() { "alpha", "beta" };

        private async void StartSort(object sender, RoutedEventArgs e)
        {
            FiveWords fiveWords = new((string)SelectFile.SelectedValue ?? files[0]);
            List<List<string>> combinations = new();
            Stopwatch timer = new();
            StartButton.IsEnabled = false;
            SelectSort.IsEnabled = false;
            SaveButton.IsEnabled = true;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimerTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            string sort = (string)SelectSort.SelectedValue ?? sorts[0];
            PercentBar.Visibility = Visibility.Visible;

            if (sort == "loop")
            {
                // fiveWords.SortForLoops();
                fiveWords.WordsCount += (sender, e) => MaxPercent = e;
                fiveWords.Progress += (sender, e) => Percent++;
                fiveWords.Combinations += (sender, e) =>
                {
                    Combos = new(Combos) { e };
                    CombosCount = Combos.Count;
                };
                await fiveWords.SortForLoops((int)Math.Floor(slider.Value));
            }
            if (sort == "recursive") { }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SelectSort.ItemsSource = sorts;
            SelectSort.SelectedIndex = 0;

            SelectFile.ItemsSource = files;
            SelectFile.SelectedIndex = 0;

            PercentBar.Visibility = Visibility.Collapsed;
        }

        public void DispatcherTimerTick(object sender, EventArgs e)
        {
            Timer.Text = string.Format("{0}", int.Parse(Timer.Text) + 1);
        }

        private void SaveToFile(object sender, RoutedEventArgs e)
        {
            using StreamWriter outputFile = new("C:\\Users\\HFGF\\source\\repos\\FiveWordFiveLetters\\Algorithms\\WriteLines.txt");
            Combos.ForEach(line => outputFile.WriteLine(line));
        }
    }
}
