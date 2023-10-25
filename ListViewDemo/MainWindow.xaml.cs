using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MovieDataAccess.Data;

namespace ListViewDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataSource DataSource { get; set; } = new DataSource();

        public MainWindowContext MainWindowContext { get; set; }
        
        public MainWindow()
        {
            MainWindowContext = new MainWindowContext();
            DataContext = MainWindowContext;
            foreach (var product in DataSource.Stock)
            {
                MainWindowContext.Products.Add(product);
            }
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.AddProduct();
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.SaveToFile();
        }

        private void LoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.LoadFromFile();
        }
    }
}
