using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MabiCookerV3
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _MainWindow = new MainWindow { MainTitleVar = "Unknown" };
        }

        MainWindow _MainWindow;

        public MainWindow MainWindow
        {
            get
            {
                return _MainWindow;
            }
            set
            {
                _MainWindow = value;
            }
        }

        public string MainTitleVar
        {
            get { return MainWindow.MainTitleVar; }
            set { 
                if (MainWindow.MainTitleVar != value)
                {
                    MainWindow.MainTitleVar = value;
                    RaisePropertyChanged("MainTitleVar");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
            System.Diagnostics.Debug.WriteLine("PropertyChanged");
        }

    }
}
