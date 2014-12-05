using System;
using System.Windows;
using System.Windows.Controls;
using MMHTPCourseProject.ViewModel;

namespace MMHTPCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPagePressure : Page
    {
        public ResultPagePressure()
        {
            InitializeComponent();
            this.DataContext = ResultPagePressureViewModel.InitializeResultPagePressureViewModel();
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Forward_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("pack://application:,,,/View/ResultPageDensity.xaml"));
        }
    }
}
