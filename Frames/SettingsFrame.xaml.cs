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

namespace MabiCookerV3
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private enum ScreenSize
        {
            size_1080p,
            size_1440p,
            size_2160p
        }

        public SettingsPage()
        {
            InitializeComponent();
            WindowSizeOption.SelectedIndex = Properties.Settings.Default.WindowSize;
        }

        private void WindowSizeOption_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var theMainWindow = (MainWindow)Application.Current.MainWindow;
            int theWindowSetting = WindowSizeOption.SelectedIndex;
            //Settings.Default.WindowSize = WindowSizeOption.Ind;
            System.Diagnostics.Debug.WriteLine("Selected Index: "+ WindowSizeOption.SelectedIndex);
            //Names.MainWindow.MainTitle.Content = "MabiCookerV3 " + MainWindow.MapString(WindowSizeOption.SelectedIndex);
            //_viewModel.MainTitleVar = "1080p";
            theMainWindow.MainTitle.Content = MainWindow.MapString(theWindowSetting);
            Properties.Settings.Default.WindowSize = theWindowSetting;

            //System.Diagnostics.Debug.WriteLine(viewModel.MainTitleVar);
            Properties.Settings.Default.Save();
        }
    }
}
