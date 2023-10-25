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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FiveWordFiveLetters;

namespace GUI
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

        public static readonly List<string> sorts = new() { "loop", "recursive"};
        public string sort = sorts[0];

        private void StartSort(object sender, RoutedEventArgs e)
        {
            FiveWords fiveWords = new("alpha");
            List<List<string>> combinations = new();

            if (sort == "loop") combinations = fiveWords.SortForLoops();
            if (sort == "recursive") combinations = fiveWords.SortRecursive();

            MessageBox.Show($"Starter programmet med {sort} algoritmen");

            CombinationsTextbox.Text = string.Join(" ", combinations);
        }

        private void ChangeSort(object sender, SelectionChangedEventArgs e)
        {
            sort = (string)SelectSort.SelectedValue;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SelectSort.ItemsSource = sorts;
            SelectSort.SelectedIndex = 0;
        }
    }
}
