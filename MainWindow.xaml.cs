using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int FULL_BAR = 245;
        private static double ONE_HUNDRED = 100.0;
        private static double BAR_LEFT_START = 323.0;

        string _mainTitleVar;
        MainWindowViewModel _viewModel;

        public static string MapString(int theValue)
        {
            switch (theValue) 
            { 
                case 0:
                    return "1080p";
                case 1:
                    return "1440p";
                case 2:
                    return "2160p";
                default:
                    return "1080p";
            }
        }

        public string MainTitleVar
        { 
            get { return _mainTitleVar; }
            set { _mainTitleVar = value; }
        }

        //MainWindowViewModel _viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainWindowViewModel)base.DataContext;


            //this.MainTitle.Content = "MabiCookerV3 " + MapString(Properties.Settings.Default.WindowSize);
            KeyboardNavigation.SetDirectionalNavigation(ControlBar, KeyboardNavigationMode.None);

            // Register KeyEvent handler
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            this.ControlBar.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            myFrame.Navigate(new SettingsPage());

            
        }
        private void ToggleButton_Checked_Settings_Page(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility = Visibility.Visible;
        }
        private void ToggleButton_Unchecked_Settings_Page(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility= Visibility.Collapsed;
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Keyboard.ClearFocus();
            Keyboard.Focus(ControlBar);
            System.Diagnostics.Debug.WriteLine(Keyboard.FocusedElement);
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    this.Left -= 1.0;
                    break;
                case Key.Right:
                    this.Left += 1.0;
                    break;
                case Key.Up:
                    this.Top -= 1.0;
                    break;
                case Key.Down:
                    this.Top += 1.0;
                    break;
                case Key.Escape:
                    Keyboard.ClearFocus();
                    break;
            }

        }

        private void Set_Button_Click(object sender, RoutedEventArgs e)
        {
            double ing1, ing2, ing3;
            double bar_end;
            double ingBarWidth1, ingBarWidth2, ingBarWidth3;

            ing1 = Parse_String(ingredientValue1.Text);
            ing2 = Parse_String(ingredientValue2.Text);
            ing3 = ONE_HUNDRED - ing1 - ing2;

            System.Diagnostics.Debug.WriteLine(ing1);
            System.Diagnostics.Debug.WriteLine(ing2);
            System.Diagnostics.Debug.WriteLine(ing3);

            ingredientValue3.Text = ing3.ToString();

            ingBarWidth1 = FULL_BAR * ing1 / 100.0;
            ingBarWidth2 = FULL_BAR * ing2 / 100.0;
            ingBarWidth3 = FULL_BAR * ing3 / 100.0;

            ingredientBar1.Width = ingBarWidth1;
            ingredientBar2.Width = ingBarWidth2;
            ingredientBar3.Width = ingBarWidth3;

            bar_end = BAR_LEFT_START + ingBarWidth1;
            Canvas.SetLeft(ingredientBar2, bar_end);

            bar_end += ingBarWidth2;

            Canvas.SetLeft(ingredientBar3, bar_end);


        }

        private double Parse_String(string theStr)
        {
            if (theStr == null || string.IsNullOrEmpty(theStr))
            {
                return 0.0;
            }
            else
            {
                if (int.TryParse(theStr, out int integerValue))
                {
                    return (double)integerValue;
                }
                else
                {
                    if (double.TryParse(theStr, out double decimalValue))
                    {
                        return decimalValue;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }


        /// <summary>
        /// Min
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// DragMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}