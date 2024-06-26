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
        // Cooking Bar size in pixels
        public static readonly int FULL_BAR = 245;
        public static readonly  double ONE_HUNDRED = 100.0;
        public static readonly double BAR_LEFT_START = 325.0;

        // Recipe for Steamed Potato
        public static readonly double POTATO_VALUE = FULL_BAR * 0.42;
        public static readonly double WATER_VALUE = FULL_BAR * 0.56;

        double _myIngVal1;
        double _myIngVal2;
        double _myIngVal3;


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

        public double myIngVal1 { get { return _myIngVal1; } set { _myIngVal1 = value; } }
        public double myIngVal2 { get { return _myIngVal2; } set { _myIngVal2 = value; } }
        public double myIngVal3 { get { return _myIngVal3; } set { _myIngVal3 = value; } }

        //MainWindowViewModel _viewModel = new MainWindowViewModel();

        public MainWindow()
        {

            _viewModel = new MainWindowViewModel();
            _viewModel.myIngVal1 = 42.0;
            _viewModel.myIngVal2 = 58.0;
            _viewModel.myIngVal3 = 0.0;

            DataContext = _viewModel;
            InitializeComponent();
            //this.DataContext = this;


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
                case Key.Enter:
                    Set_Button_Click(null, null);
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

            ing1 = Math.Clamp(Parse_String(ingredientValue1.Text), 0.0, 100.0);
            ing2 = Math.Clamp(Parse_String(ingredientValue2.Text), 0.0, 100.0 - ing1);
            ing3 = Math.Clamp(ONE_HUNDRED - ing1 - ing2, 0.0, 100.0);

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

        private void SPotato_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.myIngVal1 = 42.0;
            _viewModel.myIngVal2 = 58.0;
            _viewModel.myIngVal3 = 0.0;
            Set_Button_Click(sender, e);
        }

        private void TBasil_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.myIngVal1 = 50.0;
            _viewModel.myIngVal2 = 30.0;
            _viewModel.myIngVal3 = 20.0;
            Set_Button_Click(sender, e);
        }

        private void VCanape_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.myIngVal1 = 40.0;
            _viewModel.myIngVal2 = 50.0;
            _viewModel.myIngVal3 = 10.0;
            Set_Button_Click(sender, e);
        }
    }
}