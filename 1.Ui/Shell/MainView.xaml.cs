using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using App.ViewModels;
using Infrastructure.BaseComponents.Components.Attributes;

namespace Ui.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            // Привязываем данные главной модели представления
            var viewModel = new MainViewModel();
            DataContext = viewModel;
        }
    }
}