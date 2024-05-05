using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MabiCookerV3
{
    public class MainWindowViewModel : MainWindowViewModelBase
    {

        MainWindow _MainWindow;

        private double _myIngVal1;
        private double _myIngVal2;
        private double _myIngVal3;

        public MainWindowViewModel()
        {
            _changeIngCommand1 = new DelegateCommand(OnChangeIngCommand1, CanChangeIng1);
            _changeIngCommand2 = new DelegateCommand(OnChangeIngCommand2, CanChangeIng2);
            _changeIngCommand3 = new DelegateCommand(OnChangeIngCommand3, CanChangeIng3);
        }

        private readonly DelegateCommand _changeIngCommand1;
        private readonly DelegateCommand _changeIngCommand2;
        private readonly DelegateCommand _changeIngCommand3;
        public ICommand ChangeIngCommand1 => _changeIngCommand1;
        public ICommand ChangeIngCommand2 => _changeIngCommand2;
        public ICommand ChangeIngCommand3 => _changeIngCommand3;

        public double myIngVal1
        {
            get => _myIngVal1;
            set => SetProperty(ref _myIngVal1, value);
        }

        public double myIngVal2
        {
            get => _myIngVal2;
            set => SetProperty(ref _myIngVal2, value);
        }

        public double myIngVal3
        {
            get => _myIngVal3;
            set => SetProperty(ref _myIngVal3, value);
        }

        private void OnChangeIngCommand1(object commandParameter)
        {
            myIngVal1 = 50.0;
            _changeIngCommand1.InvokeCanExecuteChanged();
        }
        private void OnChangeIngCommand2(object commandParameter)
        {
            myIngVal2 = 50.0;
            _changeIngCommand2.InvokeCanExecuteChanged();
        }
        private void OnChangeIngCommand3(object commandParameter)
        {
            myIngVal3 = 50.0;
            _changeIngCommand3.InvokeCanExecuteChanged();
        }

        private bool CanChangeIng1(object commandParameter) { return myIngVal1 != 50.0;}

        private bool CanChangeIng2(object commandParameter) { return myIngVal2 != 50.0; }

        private bool CanChangeIng3(object commandParameter) { return myIngVal3 != 50.0; }


    }
}
