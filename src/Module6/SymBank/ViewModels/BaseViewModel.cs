using SymBank.Commands;
using SymBank.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymBank.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
        //    if (PropertyChanged != null) PropertyChanged(this,
        //        new PropertyChangedEventArgs(propertyName));
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        private BaseView _view;

        public BaseView View
        {
            get { return _view; }
            set { _view = value; }
        }

        public void CloseView()
        {
            //    if (_view != null) _view.Close();
            _view?.Close();
        }

        public RelayCommand CloseViewCommand { get; private set; }

        public BaseViewModel()
        {
            CloseViewCommand = new RelayCommand(p => CloseView());
        }
    }
}
