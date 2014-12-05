using System;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MMHTPCourseProject.ViewModel;

namespace MMHTPCourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel.InitializeMainWindowViewModel();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void About_OnClick(object sender, RoutedEventArgs e)
        {
            //MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                ColorScheme = MetroDialogColorScheme.Theme
            };

            await this.ShowMessageAsync("О программе", "Программа реализует расчет математической модели",
                MessageDialogStyle.Affirmative, mySettings);
        }

        private void Frame_OnNavigated(object sender, NavigationEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(700))
            };
            Frame.BeginAnimation(OpacityProperty, animation); 
        }
    }
}
