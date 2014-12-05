using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MMHTPCourseProject.Control;
using MMHTPCourseProject.Model;
using MMHTPCourseProject.ViewModel;

namespace MMHTPCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class InputPage : Page
    {

        public InputPage()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel.InitializeMainWindowViewModel();
        }

        private async void ShowResults_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
            var mainWindowViewModel = this.DataContext as MainWindowViewModel;
            if (mainWindowViewModel != null)
            {
                var inputData = mainWindowViewModel.InputData;
                var mathModel = new MathModel();
                var resultPagePressureViewModel = ResultPagePressureViewModel.InitializeResultPagePressureViewModel();
                resultPagePressureViewModel.Results = await mathModel.ProcessPressure(inputData);
                resultPagePressureViewModel.DeltaP = await mathModel.Process(inputData);
                var resultPageDensityViewModel = ResultPageDensityViewModel.InitializeResultPageDensityViewModel();
                resultPageDensityViewModel.Results = await mathModel.ProcessDensity(inputData);
            }

            ProgressRing.IsActive = false;
            NavigationService.Navigate(new Uri("pack://application:,,,/View/ResultPagePressure.xaml"));
        }

    }
}
