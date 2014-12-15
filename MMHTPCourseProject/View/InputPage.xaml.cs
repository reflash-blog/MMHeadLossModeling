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
                if (inputData.L >0 && inputData.L<=1000 &&                          // 0<L<=1000 
                    inputData.D >0 && inputData.D<=10 &&                            // 0<D<=10
                    inputData.N >0 && inputData.N<=25 &&                            // 0<N<=25
                    inputData.Ro>0 && inputData.Ro<=2000 &&                         // 0<Ro<=2000
                    inputData.WStart<inputData.WEnd&&                               // W1 < W2
                    inputData.WStart>0 && inputData.WEnd<=10 &&                     // 0<W<=10
                    inputData.Mu >0 && inputData.Mu<=2000 &&                        // 0<Mu<=2000
                    inputData.Nu >0 && inputData.Nu<=1000 &&                        // 0<Nu<=1000
                    inputData.Delta >0 && inputData.Delta<=100                      // 0<Delta<=100
                    )
                {
                    var mathModel = new MathModel();
                    var resultPagePressureViewModel =
                        ResultPagePressureViewModel.InitializeResultPagePressureViewModel();
                    resultPagePressureViewModel.Results = await mathModel.ProcessPressure(inputData);
                    resultPagePressureViewModel.DeltaP = await mathModel.Process(inputData);
                    var resultPageDensityViewModel = ResultPageDensityViewModel.InitializeResultPageDensityViewModel();
                    resultPageDensityViewModel.Results = await mathModel.ProcessDensity(inputData);
                    NavigationService.Navigate(new Uri("pack://application:,,,/View/ResultPagePressure.xaml"));
                }
                else
                {
                    MessageBox.Show("Введенные данные не соответствуют ограничениям\n"+
                        "0  <   L   <=  1000 \n"+
                        "0  <   D   <=  10 \n" +
                        "0  <   N   <=  25 \n" +
                        "0  <   Ro  <=  2000 \n" +
                        "W1 < W2 \n" +
                        "0  <   W   <=  10 \n" +
                        "0  <   Mu  <=  2000 \n" +
                        "0  <   Nu  <=  1000 \n" +
                        "0  <   Delta   <=  100 \n", 
                        "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            ProgressRing.IsActive = false;

        }

    }
}
