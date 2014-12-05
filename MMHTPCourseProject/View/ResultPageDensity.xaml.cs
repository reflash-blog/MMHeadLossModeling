using System;
using System.Windows;
using System.Windows.Controls;
using MMHTPCourseProject.ViewModel;

namespace MMHTPCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPageDensity : Page
    {
        public ResultPageDensity()
        {
            InitializeComponent();
            this.DataContext = ResultPageDensityViewModel.InitializeResultPageDensityViewModel();
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


    }
}
