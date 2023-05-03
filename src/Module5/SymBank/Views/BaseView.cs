using SymBank.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SymBank.Views
{
    public class BaseView : UserControl
    {
        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty RegionProperty;

        public object Header
        {
            get
            {
                return GetValue(HeaderProperty);
            }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        public Region Region
        {
            get
            {
                return (Region)GetValue(RegionProperty);
            }
            set
            {
                SetValue(RegionProperty, value);
            }
        }

        static BaseView()
        {
            HeaderProperty = DependencyProperty.Register(
                "Header", typeof(object), typeof(BaseView),
                new PropertyMetadata(null));
            RegionProperty = DependencyProperty.Register(
                "Region", typeof(Region), typeof(BaseView),
                new PropertyMetadata(Region.MainRegion));
        }

        public BaseView()
        {
            Loaded += BaseView_Loaded;
        }

        private void BaseView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm == null)
            {
                _vm = (BaseViewModel)DataContext;
                if (_vm != null) _vm.View = this;
            }
            ResetFocus();
        }

        public virtual void ResetFocus()
        {

        }

        public void Show()
        {
            Shell.Instance.Attach(this);
        }

        public void Close()
        {
            Shell.Instance.Detach(this);
        }

        private BaseViewModel _vm;

        public BaseViewModel ViewModel
        {
            //    get { return _vm; }
            get => _vm;
        }
    }
}
